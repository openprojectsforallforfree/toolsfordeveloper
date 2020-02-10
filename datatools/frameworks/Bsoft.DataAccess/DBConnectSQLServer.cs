
using System.Data.SQLite;
using Bsoft.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Bsoft.Data
{
    /// <summary>
    /// DatabaseConnection
    /// </summary>
    public class DBConnect : IDbConnect, IDisposable
    {
        #region "Private Variable declaration"

        private Int16 _loginReTryCount = 0;

        private SqlConnection _connection = new SqlConnection();
        private string _connectionString = string.Empty;

        #endregion "Private Variable declaration"

        #region Constructors and Finalizer

        /// <summary>
        /// Initializes the DBConnect
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public DBConnect(string connectionString)
        {
            this._connectionString = connectionString;

            InitParameter();
        }

        /// <summary>
        /// Initializes the DBConnect
        /// </summary>
        public DBConnect()
        {
            InitParameter();
        }

        private void InitParameter()
        {
            this._loginReTryCount = 5;
        }

        #region "IDisposable Implementation"

        private bool isAppClosing = false;

        // Pointer to an external unmanaged resource.
        private IntPtr handle;

        // Track whether Dispose has been called.
        private bool disposed = false;

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        //private void AddAppExitHandler()
        //{
        //    System.Windows.Forms.Application.ApplicationExit += OnApplicationExit;
        //}

        private void OnApplicationExit(object sender, EventArgs e)
        {
            this.isAppClosing = true;
        }

        public bool IsApplicationClosing
        {
            get { return this.isAppClosing; }
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            this.Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch { }
                }
            }
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if ((this._connection != null))
                    {
                        this._connection.Dispose();
                    }
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }
            disposed = true;
        }

        // This finalizer will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide finalize methods in types derived from this class.
        ~DBConnect()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion "IDisposable Implementation"

        #endregion Constructors and Finalizer

        #region Public propery

        /// <summary>
        /// Gets the current state of connection to the data source.
        /// </summary>
        public ConnectionState State
        {
            get
            {
                if (_connection == null)
                {
                    return ConnectionState.Closed;
                }
                else
                {
                    return _connection.State;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the connection string to the data source.
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Gets the SqlConnection object
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
        }

        public bool IsConnected
        {
            get { return DoesDBConnectionExists(); }
        }

        #endregion Public propery

        #region "Public Function Declaration"

        ///<summary>
        /// Opens the database connection
        ///</summary>
        ///<example>
        /// DBConnect con = new DBConnect(conString);
        /// con.Open();
        ///</example>
        ///<returns>
        /// Return ture is db is connected successfully else false.
        /// </returns>
        public bool Open()
        {
            try
            {
                //connection
                return this.ConnectDatabase();
            }

            catch
            {
                throw;
            }
        }

        ///<summary>
        /// Opens the database connection
        ///</summary>
        ///<example>
        /// DBConnect con = new DBConnect(conString);
        /// con.Open();
        ///</example>
        ///<returns>
        /// Return ture is db is connected successfully else false.
        /// </returns>
        public bool Open(string connectionString)
        {
            try
            {
                _connectionString = connectionString;
                //connection
                return this.ConnectDatabase();
            }

            catch
            {
                throw;
            }
        }

        ///<summary>
        /// Closes the database connection
        ///</summary>
        public void Close()
        {
            this.DisconnectDatabase();
        }

        /// <summary>
        /// Execute a commandText (that returns a resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">SQL command</param>
        /// <returns>Returns a datatable containing the resultset generated by the command</returns>
        /// <example>
        /// DataTable dt= DatabaseConnection.ExecuteTable("SELECT * FROM emp");
        /// </example>
        public DataTable ExecuteDataTable(StringBuilder commandText)
        {
            return ExecuteDataTable(commandText.ToString());
        }

        /// <summary>
        /// Execute a commandText (that returns a resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">SQL command</param>
        /// <returns>Returns a datatable containing the resultset generated by the command</returns>
        /// <example>
        /// DataTable dt= DatabaseConnection.ExecuteTable("SELECT * FROM emp");
        /// </example>
        public DataTable ExecuteDataTable(string commandText)
        {
            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            //creating the dataAdapter object to execute the dataTable.
            SqlDataAdapter dataAdapter = null;

            DataTable dt = new DataTable();
            try
            {
                //open the data adapter.
                dataAdapter = new SqlDataAdapter(commandText, _connection);

                //load the data into the dataTable.
                dt.BeginLoadData();
                dataAdapter.Fill(dt);
                dt.EndLoadData();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Trim() == "ORA-03114: NOT CONNECTED TO ORACLE" || ex.Message.ToUpper().Trim() == "ORA-12571: TNS:PACKET WRITER FAILURE")
                {
                    ////Try to db Re_Connected...
                    //if (MsgBoxNepali.Show("*]^fa];;Fu sg]S;g ePsf] %}g\\, s] tkfO{ km]<L *]^fa];;Fu sg]S;g ug]{ xf] M ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    DisconnectDatabase();
                    //    if (!ConnectDatabase())
                    //    {
                    //       // MsgBoxNepali.Show("*]^fa];;Fu sg]S;g x'g ;s]g ", MessageBoxButtons.OK);
                    //        //.Show("*]^fa];;Fu sg]S;g x'g ;s]g ", MessageBoxButtons.OK);
                    //        this.Dispose();
                    //    }
                    //}
                    //else
                    //{
                    //    this.Dispose(); //if Re-connect not do the project dispose....
                    //}
                }

                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                LogTrace.WriteDebugLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, commandText);
                throw ex;
            }
            finally
            {
                commandText = string.Empty;
                dataAdapter.Dispose();
                dataAdapter = null;
            }

            return dt;
        }

        /// <summary>
        /// Execute a commandText (that returns a resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">SQL command</param>
        /// <returns>Returns a datatable containing the resultset generated by the command</returns>
        /// <example>
        /// DataTable dt= DatabaseConnection.ExecuteTable("SELECT * FROM emp");
        /// </example>
        public DataSet ExecuteDataSet(StringBuilder commandText)
        {
            return ExecuteDataSet(commandText.ToString());
        }

        /// <summary>
        /// Execute a commandText (that returns a resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">SQL command</param>
        /// <returns>Returns a datatable containing the resultset generated by the command</returns>
        /// <example>
        /// DataTable dt= DatabaseConnection.ExecuteTable("SELECT * FROM emp");
        /// </example>
        public DataSet ExecuteDataSet(string commandText)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            //creating the dataAdapter object to execute the dataTable.
            SqlDataAdapter dataAdapter = null;

            DataSet dt = new DataSet();
            try
            {
                //open the data adapter.
                dataAdapter = new SqlDataAdapter(commandText, _connection);

                //load the data into the dataTable.
                dataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //if (false)//ex.Message.ToUpper().Trim() == "ORA-03114: NOT CONNECTED TO ORACLE" || ex.Message.ToUpper().Trim() == "ORA-12571: TNS:PACKET WRITER FAILURE")
                //{
                //    //Try to db Re_Connected...
                //    if (MessageBox.Show("*]^fa];;Fu sg]S;g ePsf] %}g\\, s] tkfO{ km]<L *]^fa];;Fu sg]S;g ug]{ xf] M ") == DialogResult.Yes)
                //    {
                //        DisconnectDatabase();
                //        if (!ConnectDatabase())
                //        {
                //            MessageBox.Show("*]^fa];;Fu sg]S;g x'g ;s]g ");
                //            // this.MsgBoxNepaliDispose();
                //        }
                //    }
                //    else
                //    {
                //        this.Dispose(); //if Re-connect not do the project dispose....
                //    }
                //}

                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                LogTrace.WriteDebugLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, commandText);
                throw ex;
            }
            finally
            {
                commandText = string.Empty;
                dataAdapter.Dispose();
                dataAdapter = null;
            }

            return dt;
        }

        /// <summary>
        /// To get the newly inserted row id
        /// </summary>
        /// <param name="commandText">sql</param>
        /// <param name="primaryKey">Primary key field eg "id" </param>
        /// <returns>The id of the newly inserted Row</returns>
        public int ExecuteInsertQuery(string commandText, string primaryKey)
        {
            //Parse insert
            //eg INSERT INTO Grading (Formula)  VALUES('0<=A<34<=B<=50')

            ExecuteNonQuery(commandText);
            string[] parse = commandText.Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string tablename = parse[2];
            int startofopBrac = 0;
            startofopBrac = commandText.IndexOf('(');
            int endofopBrac = 0;
            endofopBrac = commandText.IndexOf(')');
            string fields = commandText.Substring(startofopBrac + 1, endofopBrac - startofopBrac);
            int startOfValues = 0;
            startOfValues = commandText.IndexOf('(', endofopBrac, commandText.Length - endofopBrac);
            int endOfValues = 0;
            endOfValues = commandText.IndexOf(')', startOfValues, commandText.Length - startOfValues);
            string values = commandText.Substring(startOfValues + 1, endOfValues - startOfValues - 1);

            string[] cols = fields.Split(',');
            string[] vals = values.Split(',');
            string sql = "SELECT max(" + primaryKey + ") FROM " + tablename + " WHERE ";
            for (int i = 0; i < cols.Length - 1; i++)
            {
                sql += cols[i] + " = " + vals[i] + " AND ";
            }
            sql = sql.Substring(0, sql.Length - 5);
            int Id = Conversion.ToInt32(ExecuteScalar(sql));
            return Id;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>Returns an int representing the number of rows affected by the command.</returns>
        /// <example>
        /// int result = ExecuteNonQuery("UPdate query");
        /// </example>
        public int ExecuteNonQuery(StringBuilder commandText)
        {
            return ExecuteNonQuery(commandText.ToString());
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database
        /// </summary>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>Returns an int representing the number of rows affected by the command.</returns>
        /// <example>
        /// int result = ExecuteNonQuery("UPdate query");
        /// </example>
        public int ExecuteNonQuery(string commandText)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return -1;
            }

            if (!DoesDBConnectionExists())
            {
                return -1;
            }
            //creating a oracle command for executing the command text.
            SqlCommand cmd = GetCommand(commandText);

            int iRetVal = -1;
            try
            {
                iRetVal = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                LogTrace.WriteDebugLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, commandText);

                if (ex.Message.ToUpper().Trim() == "no connections")
                {
                }
                else if (ex.Message.Contains("is not unique"))
                {
                    throw new Exception("The data should be Unique it cannot be repeated .");
                }
                else if (ex.Message.Contains("foreign key constraint failed"))
                {
                    throw new Exception("Foreign key constraint failed or The data is used in other table so cannot delete it");
                }
                else if (ex.Message.Contains("Abort due to constraint violation"))
                {
                    string message = ex.Message;
                    message = message.Replace("Abort due to constraint violation", "");
                    message = message.Replace("columns", "");
                    message = message.Replace("are not unique", "should be unique.\n Please use the existing data or Input unique data.");
                    throw new Exception(message);
                }


                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                        String.Format("Error in query . " + commandText + ex.StackTrace));
                throw ex;
            }
            finally
            {
                commandText = string.Empty;
                commandText = null;
                DisposeObject(cmd);
            }
            return iRetVal;
        }

        /// <summary>
        ///Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="commandText">the CommandType (stored procedure, text, etc.)</param>
        /// <returns>Returns a dataReader.</returns>
        public SqlDataReader ExecuteDataReader(string commandText)
        {
            return ExecuteDataReader(commandText, CommandBehavior.Default);
        }

        /// <summary>
        ///Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="commandText">the CommandType (stored procedure, text, etc.)</param>
        /// <returns>Returns a dataReader.</returns>
        public SqlDataReader ExecuteDataReader(StringBuilder commandText)
        {
            return ExecuteDataReader(commandText.ToString(), CommandBehavior.Default);
        }

        /// <summary>
        ///Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="commandText">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="behaviour">Behaviour of how the data reader will act.</param>
        /// <returns>Returns a dataReader.</returns>
        public SqlDataReader ExecuteDataReader(string commandText, CommandBehavior behaviour)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            //check for DB connectivity
            if (!DoesDBConnectionExists())
            {
                return null;
            }

            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            try
            {
                cmd = GetCommand(commandText);
                rdr = cmd.ExecuteReader(behaviour);
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                        String.Format("Error in query . " + commandText + ex.Message));
            }
            finally
            {
                commandText = string.Empty;
                DisposeObject(cmd);
            }

            return rdr;
        }

        /// <summary>
        /// Creates an SqlCommand and returns the object.
        /// </summary>
        /// <param name="commandText">Command Text.</param>
        /// <returns>Reutrns an object of oracle command.</returns>
        public SqlCommand GetCommand(string commandText)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this._connection;
            cmd.CommandText = commandText;
            commandText = string.Empty;
            return cmd;
        }

        /// <summary>
        /// Creates an SqlCommand and returns the object.
        /// </summary>
        /// <returns>Reutrns an object of oracle command.</returns>
        public SqlCommand GetCommand()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this._connection;
            return cmd;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="commandText">The query statement or stored procedure name to execute</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// if the result set is empty.
        /// </returns>
        public object ExecuteScalar(string commandText)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            object obj = null;
            SqlCommand cmd = null;

            try
            {
                cmd = GetCommand(commandText);
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                         String.Format("Error in query . " + commandText + ex.StackTrace));
                MessageBox.Show("Error in query . " + commandText + ex.StackTrace);
                throw;
            }
            finally
            {
                commandText = string.Empty;
                DisposeObject(cmd);
            }

            return obj;
        }

        /// <summary>
        /// Starts a database transaction and returns the transaction Object.
        /// </summary>
        /// <returns>An System.Data.SQLite.SQLiteTransaction object representing the new transaction.</returns>
        public SqlTransaction BeginTransaction()
        {
            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            SqlTransaction trans = null;
            try
            {
                trans = this._connection.BeginTransaction();
            }
            catch
            {
            }

            return trans;
        }

        /// <summary>
        /// Ends a database transaction.
        /// </summary>
        /// <param name="trans">The System.Data.SQLite.SQLiteTransaction object representing the new transaction.</param>
        public void CommitTransaction(ref SqlTransaction pTransaction)
        {
            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return;
            }

            if (!DoesDBConnectionExists())
            {
                return;
            }
            try
            {
                pTransaction.Commit();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Rolls back (Undo changes) all the modification done in the specified transaction.
        /// </summary>
        /// <param name="trans">The System.Data.SQLite.SQLiteTransaction object representing the new transaction.</param>
        public void RollbackTransaction(ref SqlTransaction pTransaction)
        {
            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return;
            }

            if (!DoesDBConnectionExists())
            {
                return;
            }
            try
            {
                pTransaction.Rollback();
            }
            catch
            {
            }
        }

        public static string GetStringData(DataRow dRow, string columnName)
        {
            if ((dRow.IsNull(columnName)))
            {
                return string.Empty;
            }
            else
            {
                return (string)dRow[columnName];
            }
        }

        public static string GetStringData(DataRow dRow, int columnNo)
        {
            if ((dRow.IsNull(columnNo)))
            {
                return string.Empty;
            }
            else
            {
                return (string)dRow[columnNo];
            }
        }

        public static int GetIntegerData(DataRow dRow, string columnName)
        {
            int retval = 0;
            if ((dRow.IsNull(columnName)))
            {
                retval = 0;
            }
            else
            {
                if ((Validation.IsNumeric(dRow[columnName].ToString())))
                {
                    retval = (int)dRow[columnName];
                }
            }
            return retval;
        }

        public static int GetIntegerData(DataRow dRow, int columnNo)
        {
            int retval = 0;
            if ((dRow.IsNull(columnNo)))
            {
                retval = 0;
            }
            else
            {
                if ((Validation.IsNumeric(dRow[columnNo].ToString())))
                {
                    retval = (int)dRow[columnNo];
                }
            }
            return retval;
        }

        #endregion "Public Function Declaration"

        #region "Private function declaration"

        public bool DoesDBConnectionExists()
        {
            if ((this._connection.State == ConnectionState.Closed
                || this._connection.State == ConnectionState.Broken
                || this._connection.State == ConnectionState.Connecting))
            {
                if (ConnectDatabase())
                    return true;
                else
                    return false;
            }
            return true;
        }

        // Disconnect oracle database
        private void DisconnectDatabase()
        {
            if ((this._connection != null))
            {
                if (this._connection.State == ConnectionState.Open)
                {
                    this._connection.Close();
                }
            }
        }

        // Connect oracle database
        private bool ConnectDatabase()
        {
            try
            {
                //if already connected then no need to reconnect.
                if (_connection.State == ConnectionState.Open)
                    return true;

                this._connection = new SqlConnection();

                this._connection.ConnectionString = this._connectionString;
                //Me.GetConnectionString(Me.m_DBType)

                int i = 0;
                for (i = 0; i <= this._loginReTryCount - 1; i += i + 1)
                {
                    LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                            String.Format("Trying to connect db. Count = {0}", i));
                    this._connection.Open();

                    if (this._connection.State == ConnectionState.Open)
                    {
                        //Me._dataBusiness.SetConnection(Me._connection)
                        return true;
                    }
                }

                //Open database error!
                throw new Exception("Error while opening DB.");
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                           String.Format("Error connecting to database. " + ex.Message));
                MessageBox.Show(ex.Message);
                //throw new Exception("Error while opening DB.");
                return false;    //display Nepali Message..
                // ExportException(ex.Message, -3, ex)
            }
        }

        // This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        // to the provided command.
        // Parameters:
        // -command - the SqlCommand to be prepared
        // -connection - a valid SqlConnection, on which to execute this command
        // -transaction - a valid SqlTransaction, or 'null'
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-SQL command
        // -commandParameters - an array of SqlParameters to be associated with the command or 'null' if no parameters are required
        private SqlCommand PrepareCommand(CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!this.DoesDBConnectionExists())
            {
                return null;
            }

            //Creating command and associate the connection with the command
            SqlCommand command = this._connection.CreateCommand();

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            //If Not (transaction Is Nothing) Then
            // command.Transaction = transaction
            //End If

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if ((commandParameters != null))
            {
                AttachParameters(command, commandParameters);
            }

            commandText = string.Empty;

            return command;
        }

        //PrepareCommand

        // This method is used to attach array of SqlParameters to a SqlCommand.
        // This method will assign a value of DbNull to any parameter with a direction of
        // InputOutput and a value of null.
        // This behavior will prevent default values from being used, but
        // this will be the less common case than an intended pure output parameter (derived as InputOutput)
        // where the user provided no input value.
        // Parameters:
        // -command - The command to which the parameters will be added
        // -commandParameters - an array of SqlParameter tho be added to command
        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if (p.Direction == ParameterDirection.InputOutput & p.Value == null)
                {
                    p.Value = null;
                }
                command.Parameters.Add(p);
            }
        }

        //AttachParameters

        #endregion "Private function declaration"

        #region Object dispose form datageneral.cs

        public static void DisposeObject(SqlDataReader dataRdr)
        {
            if ((dataRdr != null))
            {
                dataRdr.Close();
                dataRdr.Dispose();
                dataRdr = null;
            }
        }

        public static void DisposeObject(SqlCommand cmd)
        {
            if ((cmd != null))
            {
                cmd.CommandText = string.Empty;
                cmd.Dispose();
                cmd = null;
            }
        }

        #endregion Object dispose form datageneral.cs

        /// <summary>
        ///
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns>count of records</returns>
        public int GetRecordCount(string tablename)
        {

            return Conversion.ToInt32(ExecuteScalar("select count(*) from {0}".With(tablename)));
        }

        //the input dates are in format yyyymmdd
        public static string GetDaysColumnSQL(string fromDate, string toDate)
        {
            string s = @"julianday(substr({0},1,4) || '-' || substr({0},5,2) || '-' ||  substr({0},7,2)) -
                        julianday(substr({1},1,4) || '-' || substr({1},5,2) || '-' ||  substr({1},7,2))";
            return string.Format(s, fromDate, toDate);
        }


        public int? UpdateDt(DataTable Dt, string commandText)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(commandText, _connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.UpdateCommand = cb.GetUpdateCommand();
                da.InsertCommand = cb.GetInsertCommand();
                da.DeleteCommand = cb.GetDeleteCommand();
                da.Update(Dt);
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                         String.Format("Error in query . " + commandText + ex.StackTrace));
                throw;
            }
            finally
            {
                commandText = string.Empty;
            }

            return Dt.Rows.Count;
        }

        public int? UpdateDtfast(DataTable Dt, string commandText)
        {

            //if application is exiting the don't perform DB process
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            try
            {
                //using (var cmd = new SqlCommand("",_connection))
                //{
                using (var transaction = _connection.BeginTransaction())
                {
                    SqlDataAdapter da = new SqlDataAdapter(commandText, _connection);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    da.UpdateCommand = cb.GetUpdateCommand();
                    da.InsertCommand = cb.GetInsertCommand();
                    da.DeleteCommand = cb.GetDeleteCommand();
                    da.Update(Dt);
                    transaction.Commit();
                }
                //}
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                         String.Format("Error in query . " + commandText + ex.StackTrace));
                throw;
            }
            finally
            {
                commandText = string.Empty;
            }

            return Dt.Rows.Count;
        }

        public bool? BulkCopy(DataTable dt)
        {
            if (this.isAppClosing)
            {
                return null;
            }

            if (!DoesDBConnectionExists())
            {
                return null;
            }

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connection))
                {
                    bulkCopy.DestinationTableName = dt.TableName;
                    try
                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                         String.Format("Error in query . " + "Bulk Load" + ex.StackTrace));
                throw;
            }
            return true;
        }
    }
}
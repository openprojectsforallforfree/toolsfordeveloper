using System.Collections.Generic;
using Bsoft.Common;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace Bsoft.Data
{
    public class DBStructure
    {
        #region Member variables.
        private IDbConnect _con = null;
        public string _SrvDate = string.Empty;
        public DatabaseType dbKind = DatabaseType.SQLServer;
        #endregion Member variables.

        #region Constructors & Finalizers.

        public DBStructure(string connectionString)
        {
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    _con = new DBConnect(connectionString);
                    break;

                case DatabaseType.SQLite:
                    _con = new DBConnectSQLite(connectionString);

                    break;
            }

            _con.Open();
        }

        #endregion Constructors & Finalizers.

        #region Properties

        public IDbConnect Con
        {
            get { return _con; }
            set { _con = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Check if the View exists or not.
        /// </summary>
        /// <param name="tableName">View name of which existence has to be checked.</param>
        /// <returns>Returns true if View exists else false.</returns>
        /// <author>Dhiraj</author>
        public bool DoesViewExists(string viewName)
        {
            viewName = viewName.Trim();
            DataTable dt = null;
            string sql = string.Empty;
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    sql = string.Format("select * from sys.views where name ='{0}'", viewName);
                    break;

                case DatabaseType.SQLite:
                    sql = string.Format("SELECT * FROM sqlite_master where type='view' and name ='{0}'  ", viewName);

                    break;
            }

            // select * from sys.views where name ='vwMyiew'
            dt = _con.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the table exists or not.
        /// </summary>
        /// <param name="tableName">table name of which existence has to be checked.</param>
        /// <returns>Returns true if table exists else false.</returns>
        public bool DoesTableExists(string tableName)
        {
            // tableName = tableName.Trim().ToUpper();
            DataTable dt = null;
            string sql = "";
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    sql = string.Format("select * from sys.tables where name ='{0}'", tableName); sql = string.Format("select * from sys.tables where name ='{0}'", tableName);
                    break;

                case DatabaseType.SQLite:
                    sql = string.Format("SELECT * FROM sqlite_master where type='table' and name ='{0}'  ", tableName);

                    break;
            }

            try
            {
                dt = _con.ExecuteDataTable(sql);
            }
            catch
            {
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get All Veiws
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public DataTable GetAllViews()
        {
            DataTable dt = null;
            string sql = string.Empty;
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    sql = string.Format("select * from sys.views ");
                    break;

                case DatabaseType.SQLite:
                    sql = string.Format("SELECT * FROM sqlite_master where type='view'  ");

                    break;
            }

            // select * from sys.views where name ='vwMyiew'
            dt = _con.ExecuteDataTable(sql);

            return dt;
        }

        public DataTable GetAllTableAndViews()
        {
            DataTable dt = null;
            string sql = string.Empty;
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    sql = string.Format("select * from sys.views ");
                    break;

                case DatabaseType.SQLite:
                    sql = string.Format("SELECT * FROM sqlite_master   ");

                    break;
            }

            // select * from sys.views where name ='vwMyiew'
            dt = _con.ExecuteDataTable(sql);

            return dt;
        }

        public DataTable GetAllTables()
        {
            DataTable dt = null;
            string sql = string.Empty;
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    sql = string.Format("select * from sys.tables ");
                    break;

                case DatabaseType.SQLite:
                    sql = string.Format("SELECT * FROM sqlite_master where  type='table' and name <>'sqlite_sequence';  ");

                    break;
            }

            // select * from sys.views where name ='vwMyiew'
            dt = _con.ExecuteDataTable(sql);

            return dt;
        }

        /// <summary>
        /// Check if the column name exists in the specified column or not.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns>Returns true if exists else false</returns>
        public bool DoesColumnExists(string tableName, string columnName)
        {
            return DoesColumnExists(tableName, columnName, string.Empty, 0, 0, 0);
        }

        /// <summary>
        /// Check if the column name exists in the specified column or not.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <returns>Returns true if exists else false</returns>
        public bool DoesColumnExists(string tableName, string columnName, string dataType)
        {
            return DoesColumnExists(tableName, columnName, dataType, 0, 0, 0);
        }

        /// <summary>
        /// Check if the column name exists in the specified column or not.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength">Data Length</param>
        /// <param name="dataPrecision">Data precision fo Number data type</param>
        /// <param name="dataScale">Data scale for Number datatype</param>
        /// <returns>Returns true if exists else false</returns>
        public bool DoesVarchar2ColumnExists(string tableName, string columnName, int dataLength)
        {
            return DoesColumnExists(tableName, columnName, "VARCHAR2", dataLength, 0, 0);
        }

        /// <summary>
        /// Check if the column name exists in the specified column or not.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataPrecision">Data precision fo Number data type</param>
        /// <param name="dataScale">Data scale for Number datatype</param>
        /// <returns>Returns true if exists else false</returns>
        public bool DoesNumberColumnExists(string tableName, string columnName, int dataPrecision, int dataScale)
        {
            return DoesColumnExists(tableName, columnName, "NUMBER", 0, dataPrecision, dataScale);
        }

        /// <summary>
        /// Check if the column name exists in the specified column or not.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength">Data Length</param>
        /// <param name="dataPrecision">Data precision fo Number data type</param>
        /// <param name="dataScale">Data scale for Number datatype</param>
        /// <returns>Returns true if exists else false</returns>
        ///
        public bool DoesColumnExists(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            bool val = false;
            switch (dbKind)
            {
                case DatabaseType.SQLServer:
                    val = doescolumnExistSQLServer(tableName, columnName, dataType, dataLength, dataPrecision, dataScale);
                    break;

                case DatabaseType.SQLite:
                    val = doescolumnExistSQLite(tableName, columnName, dataType, dataLength, dataPrecision, dataScale);
                    break;
            }
            return val;
        }

        private bool doescolumnExistSQLServer(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            tableName = tableName.Trim().ToUpper();
            if (tableName.Length == 0 || columnName.Trim().Length == 0)
                throw new Exception("Both tableName and columnName must be passed");
            DataTable dt = null;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT * FROM sys.columns WHERE Object_ID = Object_ID('{0}') AND  name = '{1}'", tableName, columnName.Trim().ToUpper());
            if (dataType.Trim().Length > 0)
            {
                //Comment coz..
                //oracle data_length is show the different in data type varchar case
                // any one Table all the Column Name is Unique so that there do not require the check data length
                sql.AppendFormat("\n\t AND data_type = '{0}'", dataType.Trim().ToUpper());
                if (dataLength > 0)
                    sql.AppendFormat("\n\t AND data_length = {0}", dataLength);
                if (dataPrecision > 0)
                {
                    sql.AppendFormat("\n\t AND data_precision = {0}", dataPrecision);
                    sql.AppendFormat("\n\t AND data_scale = {0}", dataScale);
                }
            }
            dt = _con.ExecuteDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private string GetDataType(string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            string str = string.Empty;
            if (dataType.StartsWith("NUMBER"))
            {
                return string.Format("NUMBER({0},{1}", dataPrecision, dataScale);
            }
            if (dataType.StartsWith("VARCHAR") || dataType.StartsWith("CHAR"))
            {
                return string.Format("VARCHAR({0}", dataLength);
            }
            if (dataType.StartsWith("DATE"))
            {
                str = "DATE";
            }
            return str;
        }

        private bool doescolumnExistSQLite(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            dataType = dataType.ToUpper().Trim();
            string str = this.GetDataType(dataType, dataLength, dataPrecision, dataScale);
            DataTable table = _con.ExecuteDataTable(string.Format("PRAGMA TABLE_INFO({0});", tableName));
            if ((table != null) & (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    if (DBConnectSQLite.GetStringData(row, "name").ToUpper() == columnName.ToUpper())
                    {
                        str = str.Trim();
                        return ((str.Length == 0) || (DBConnectSQLite.GetStringData(row, "type") == str));
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the object exists or not.
        /// </summary> this function is not use
        /// <param name="sequenceName">object name of which existence has to be checked.</param>
        /// <returns>Returns true if object exists else false.</returns>
        ///
        public bool DoesSequenceExists(string objectName)
        {
            return DoesObjectExists(objectName, "SEQUENCE");
        }

        /// <summary>
        /// Check if the object exists or not.
        /// </summary>
        /// <param name="objectName">object name of which existence has to be checked.</param>
        /// <param name="objectType">object type.</param>
        /// <returns>Returns true if object exists else false.</returns>
        public bool DoesConstraintExists(string constraintName, string tableName)
        {
            constraintName = constraintName.Trim().ToUpper();
            tableName = tableName.Trim().ToUpper();

            if (constraintName.Length == 0 || tableName.Length == 0)
                throw new Exception("Both constraintName and tableName must be passed.");

            DataTable dt = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_constraints");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t constraint_name = '{0}'", constraintName);
            sql.AppendFormat("\n\t AND table_name = '{0}'", tableName);

            dt = _con.ExecuteDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the object exists or not.
        /// </summary>
        /// <param name="objectName">object name of which existence has to be checked.</param>
        /// <param name="objectType">object type.</param>
        /// <returns>Returns true if object exists else false.</returns>
        public bool DoesObjectExists(string objectName, string objectType)
        {
            objectType = objectType.Trim().ToUpper();
            objectName = objectName.Trim().ToUpper();

            if (objectName.Length == 0 || objectType.Length == 0)
                throw new Exception("Both objectName and objectType must be passed.");

            DataTable dt = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_objects ");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t object_name = '{0}'", objectName);
            sql.AppendFormat("\n\t AND object_type = '{0}'", objectType);

            dt = _con.ExecuteDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// NOW Add colum value pass only table name columname and data type and datalength, dataPrecision
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength"></param>
        /// <param name="dataPrecision"></param>
        /// <param name="dataScale"></param>
        /// <returns></returns>
        public bool AddVarchar2Column(string tableName, string columnName, int dataLength)
        {
            return AddColumn(tableName, columnName, "VARCHAR2", dataLength, 0, 0);
        }

        /// <summary>
        /// NOW Add colum value pass only table name columname and data type and datalength, dataPrecision
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength"></param>
        /// <param name="dataPrecision"></param>
        /// <param name="dataScale"></param>
        /// <returns></returns>
        public bool AddColumn(string tableName, string columnName, string dataType)
        {
            return AddColumn(tableName, columnName, dataType, 0, 0, 0);
        }

        /// <summary>
        /// NOW Add colum value pass only table name columname and data type and datalength, dataPrecision
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength"></param>
        /// <param name="dataPrecision"></param>
        /// <param name="dataScale"></param>
        /// <returns></returns>
        public bool AddNumberColumn(string tableName, string columnName, int dataPrecision, int dataScale)
        {
            return AddColumn(tableName, columnName, "NUMBER", 0, dataPrecision, dataScale);
        }

        /// <summary>
        /// NOW Add colum value pass only table name columname and data type and datalength, dataPrecision
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dataLength"></param>
        /// <param name="dataPrecision"></param>
        /// <param name="dataScale"></param>
        /// <returns></returns>
        public bool AddColumn(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {//jan 25  2010
            bool flag = false;
            switch (dbKind)
            {
                case DatabaseType.SQLite:
                    flag = AddColumnSqlserver(tableName, columnName, dataType, dataLength, dataPrecision, dataScale);
                    break;

                case DatabaseType.SQLServer:
                    flag = AddColumnSqlserver(tableName, columnName, dataType, dataLength, dataPrecision, dataScale);
                    break;
            }
            return flag;
        }

        //01-May-2009

        private bool AddColumnSqlserver(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            tableName = tableName.Trim().ToUpper();
            if (tableName.Length == 0)
                throw new Exception("tableName must be passed");

            if (columnName.Length == 0)
                throw new Exception("columnName must be passed");

            if (dataType.Length == 0)
                throw new Exception("dataType must be passed");

            if (!DoesColumnExists(tableName, columnName, dataType, dataLength, dataPrecision, dataScale))
            {
                if (_con.State == ConnectionState.Open)
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.Append("ALTER TABLE ");
                    Sql.AppendFormat("{0}", tableName);
                    Sql.Append(" ADD ");
                    Sql.AppendFormat("{0}", columnName);
                    Sql.AppendFormat("  {0}", dataType);
                    if (dataLength > 0)
                    {
                        Sql.AppendFormat(" ({0})", dataLength);
                    }

                    if (dataPrecision > 0)
                    {
                        Sql.AppendFormat(" ({0}", dataPrecision);
                        if (dataScale > 0)
                        {
                            Sql.AppendFormat(", {0}", dataScale);
                        }
                        Sql.Append(")");
                    }

                    int result = _con.ExecuteNonQuery(Sql.ToString());
                    if (result > 0)
                    {
                        LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name
                            , string.Format("New column with following information added successfully. tableName = {0}, columnName ={1}, dataType = {2}, dataLength = {3}, dataPrecision =, int dataScale.",
                                tableName, columnName, dataType, dataLength, dataPrecision, dataScale));
                    }
                    return true;
                }
            }
            return false;
        }

        private bool AddColumnSqlite(string tableName, string columnName, string dataType, int dataLength, int dataPrecision, int dataScale)
        {
            bool flag = false;
            string str = this.GetDataType(dataType, dataLength, dataPrecision, dataScale);
            try
            {
                if (this.DoesColumnExists(tableName, columnName))
                {
                    return flag;
                }
                string commandText = string.Empty;
                commandText = string.Format("ALTER TABLE {0} ADD COLUMN {1}", tableName, str);
                if (_con.ExecuteNonQuery(commandText) > 0)
                {
                    flag = true;
                }
                LogTrace.WriteInfoLog(base.GetType().Name, MethodBase.GetCurrentMethod().Name, string.Format("COLUMN [{0}] not found in TABLE [{1}]. Its added in the table [{1}] successfully.", columnName, tableName));
            }
            catch (Exception exception)
            {
                LogTrace.WriteErrorLog(base.GetType().Name, MethodBase.GetCurrentMethod().Name, string.Format("COLUMN [{0}] not found in TABLE [{1}]. But could not add the COLUMN [{0}] due to some error {2}", columnName, tableName, exception.ToString()));
            }
            return flag;
        }

        public bool TableSequenceCreate(string tableName, string ColName)
        {
            if (DoesTableExists(tableName))
            {
                if (!DoesObjectExists(tableName + "_SEQ", "SEQUENCE"))
                {
                    //if SEQUENCE does not exists then create new sequence
                    if (TableColumnMaxValue(tableName, ColName, false) != string.Empty)
                    {
                        SequencCreateSelectTable(tableName + "_SEQ", TableColumnMaxValue(tableName, ColName, false).ToString());
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// pass the table name and column Name and return max value;
        /// Waring: only colnmae pass particular one filed search
        /// MultipleColumn is true the string ColumnName is not working
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="ColumnName"></param>
        /// <param name="MultipleColumn"></param>
        /// <returns></returns>
        private string TableColumnMaxValue(string tablename, string ColumnName, bool OnlySNIdColumn)
        {
            string tblName = tablename.ToUpper().ToString();
            string colName = ColumnName.ToUpper().ToString(); //Waring: only colnmae pass particular one filed search
            bool SnIdcol = OnlySNIdColumn; //Warning this value true only Sn col and ID Col search

            //if MultipleColumn is true the ColumnName is not working

            StringBuilder SqlMaxVal = new StringBuilder();
            string MaxValue = string.Empty;

            SqlMaxVal.Remove(0, SqlMaxVal.Length);
            SqlMaxVal.Append("SELECT");
            SqlMaxVal.Append("\n\tColumn_Name ");
            SqlMaxVal.Append("\nFROM");
            SqlMaxVal.Append("\n\tuser_tab_columns");
            SqlMaxVal.Append("\nWHERE");
            SqlMaxVal.AppendFormat("\ntable_Name = '{0}'", tblName);

            if (SnIdcol)
            {
                SqlMaxVal.Append("\nAND (Column_name = 'ID' OR Column_name = 'SN')");
            }
            else
            {
                SqlMaxVal.AppendFormat("\nAND Column_Name = '{0}'", colName);
            }
            SqlMaxVal.Append("\nAND Data_Type = 'NUMBER'");

            DataTable rsMaxVal = null; //set null value

            string IDCol = string.Empty;
            rsMaxVal = _con.ExecuteDataTable(SqlMaxVal.ToString());

            if (rsMaxVal != null && rsMaxVal.Rows != null && rsMaxVal.Rows.Count > 0)
            {
                IDCol = rsMaxVal.Rows[0]["Column_Name"].ToString();

                SqlMaxVal.Remove(0, SqlMaxVal.Length);
                SqlMaxVal.Append("SELECT");

                SqlMaxVal.Append("\n\tNVL(MAX(");
                SqlMaxVal.Append(IDCol); //get the max value id cols
                SqlMaxVal.Append(" ), 0) + 1 VAL");
                SqlMaxVal.Append("\nFROM");
                SqlMaxVal.AppendFormat("\n {0}", tablename);

                DataTable rsVal = null; //set null value.
                rsVal = _con.ExecuteDataTable(SqlMaxVal.ToString());

                if (rsVal != null && rsVal.Rows != null && rsVal.Rows.Count > 0)
                {
                    MaxValue = rsVal.Rows[0]["VAL"].ToString();
                }
                return MaxValue;
            }
            else
                return string.Empty; //set null if data id or sn is not found
        }

        public bool DropIfExist(string tableName)
        {
            string s = @"IF EXISTS(SELECT 1 FROM sysobjects WHERE type = 'U' and name = '{0}')
  drop table {0}".With(tableName);
            _con.ExecuteNonQuery(s);
            return true;
        }

        public bool CreateTABLE(DataTable table, bool addIDkey)
        {
            string
            sqlsc = "CREATE TABLE " + table.TableName + "(";
            if (addIDkey)
            {
                sqlsc += string.Format("\n {0}Id integer primary key identity(1,1) ,", table.TableName + "_Pk");
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n [" + table.Columns[i].ColumnName + "] ";
                if (table.Columns[i].DataType.ToString().Contains("System.Int32"))
                    sqlsc += " int ";
                else if (table.Columns[i].DataType.ToString().Contains("System.DateTime"))
                    sqlsc += " datetime ";
                else
                {
                    if (table.Columns[i].MaxLength > 0)
                    {
                        sqlsc += " nvarchar(" + table.Columns[i].MaxLength.ToString() + ") ";
                    }
                    else
                    {
                        sqlsc += " nvarchar(max) ";
                    }
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }


            string sql = sqlsc.Substring(0, sqlsc.Length - 1) + ")";
            _con.ExecuteNonQuery(sql);
            return true;
        }

        public bool CreateTableFirstRowIsType(DataTable table, bool addIDkey)
        {
            List<string> columns = new List<string>();
            if (addIDkey)
            {
               columns.Add(   string.Format("\n {0}Id integer primary key identity(1,1) ", table.TableName + "_Pk"));
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                columns.Add(  string.Format("\n [{0}] {1} ", table.Columns[i].ColumnName, table.Rows[0][i].ToString()));
            }
            string sqlsc =string.Format(  "CREATE TABLE {0} ({1}) " , table.TableName , string.Join(",", columns.ToArray()));
            _con.ExecuteNonQuery(sqlsc);
            return true;
        }


        //public bool AllTableGenerateSequence()
        //{
        //    StringBuilder sql = new StringBuilder();
        //    StringBuilder SqlSeq = new StringBuilder();

        //    //Get the all table
        //    sql.Append("SELECT");
        //    sql.Append("\n\tTABLE_NAME, Length(TABLE_NAME) leng ");
        //    sql.Append("\nFROM");
        //    sql.Append("\n\tTabs");
        //    sql.Append("\n\tORDER BY TABLE_NAME");
        //    DataTable dt = null;

        //    //get the al sequence information
        //    SqlSeq.Append("SELECT");
        //    SqlSeq.Append("\n\tOBJECT_NAME ");
        //    SqlSeq.Append("\nFROM");
        //    SqlSeq.Append("\n\tuser_objects");
        //    SqlSeq.Append("\nWHERE");
        //    SqlSeq.AppendFormat("\nOBJECT_TYPE = '{0}'", "SEQUENCE");

        //    DataTable rsTemp = null;
        //    DataRow[] rows = null; //checking Sequence exists or not

        //    string tblName = string.Empty;
        //    string colName = string.Empty;
        //    int counttble = 0;

        //    dt = _con.ExecuteDataTable(sql.ToString());
        //    rsTemp = _con.ExecuteDataTable(SqlSeq.ToString());

        //    if (dt.Rows.Count > 1)
        //    {
        //        int i = 0;
        //        while (i < dt.Rows.Count)
        //        {
        //            //set the table name
        //            tblName = string.Empty; //first set the blank after add the table name;;
        //            tblName = dt.Rows[i]["TABLE_NAME"].ToString();

        //            int vLenght = 0;
        //            bool res = false;
        //            string vMaxValue = string.Empty;
        //            string SeqName = string.Empty;

        //            vLenght = Convert.ToInt32(dt.Rows[i]["leng"].ToString());

        //            if (vLenght >= 21)
        //            {
        //                //"FYWISECONSTRUCTIONEVALRATES") there is auto create table get fist left = 15 length
        //                SeqName = tblName.Substring(0, 15).ToString(); //greate than 23 table name reduse the sequence name exists
        //                SeqName = SeqName + "_SEQ";
        //                rows = null;
        //                rows = rsTemp.Select("OBJECT_NAME = '" + SeqName.ToUpper().ToString() + "'");
        //                if (rows != null && rows.Length < 1)
        //                {
        //                    vMaxValue = TableColumnMaxValue(tblName, string.Empty, true); //get the max value.

        //                    if (vMaxValue != string.Empty) //value Return only if tale column is found id/sn col
        //                    {
        //                        res = SequencCreateSelectTable(SeqName.ToString(), vMaxValue);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                rows = null;
        //                SeqName = tblName + "_SEQ";

        //                rows = rsTemp.Select("OBJECT_NAME = '" + SeqName.ToUpper().ToString() + "'");
        //                if (rows != null && rows.Length < 1)
        //                {
        //                    vMaxValue = TableColumnMaxValue(tblName, string.Empty, true); //get the max value.
        //                    if (vMaxValue != string.Empty) //value Return only if tale column is found id/sn col
        //                    {
        //                        vMaxValue = Math.Truncate(Convert.ToDouble(vMaxValue)).ToString();

        //                        res = SequencCreateSelectTable(SeqName, vMaxValue);
        //                    }
        //                }
        //            }
        //            i++;
        //        }
        //        DataGeneral.DisposeObject(dt);
        //    }
        //    return false;
        //}
        /// <summary>
        /// if AllTableSequenceExi value true then sequence not check direct create the sequence
        /// only AllTableSequenceExi false if you cretea the only one sequence
        /// </summary> Table Name Means = Sequence Name
        /// <param name="tableName"></param>
        /// <param name="Maxval"></param>
        /// <param name="AllTableSequenceExi"></param>
        /// <returns></returns>
        public bool SequencCreateSelectTable(string tableName_Sequence, string Maxval)
        {
            if (_con.State == ConnectionState.Open)
            {
                StringBuilder sql = new StringBuilder();
                string SEQ_Name = string.Empty;
                SEQ_Name = tableName_Sequence.ToUpper().Trim();

                sql.Remove(0, sql.Length);
                sql.Append("CREATE SEQUENCE");
                //some table name too long so that sequnce is not create

                sql.AppendFormat(" {0}", SEQ_Name);
                sql.Append("\nINCREMENT BY 1");
                sql.Append("\nNOMINVALUE");
                sql.AppendFormat("\nSTART WITH {0}", Maxval);
                sql.Append("\nNOMAXVALUE");
                sql.Append("\nNOCYCLE");

                int res = _con.ExecuteNonQuery(sql.ToString());
                if (res > 0)
                {
                    //Sequence create success.
                    return true;
                }
            }
            return false;
        }

        public bool WrongFiscalYearUpdate()
        {
            StringBuilder Sql = new StringBuilder();
            //UPDATE SuperAdmin_FISCALYEARS SET DateTo = '2065/03/31'
            //WHERE FISCALYEAR = '2064/2065'

            Sql.Append("UPDATE SuperAdmin_FISCALYEARS SET");
            Sql.AppendFormat("\nDateTo = '{0}'", "2065/03/31");
            Sql.Append("\nWHERE");
            Sql.AppendFormat("\nFISCALYEAR = '{0}'", "2064/2065");
            Sql.Append(" AND DATEFROM  ='2064/04/01' AND DATETO  ='2065/03/32'");
            int Result = _con.ExecuteNonQuery(Sql.ToString());
            if (Result > 0)
            {
                //Update success..
                return true;
            }
            return false;
        }

        /// <summary>
        /// if rights name does not exists the insert new rights name colum
        /// </summary>
        /// <returns></returns>
        public bool RightsnNameInsert()
        {
            bool Status = false;
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT ");
            SQL.Append("\nSN, RightsName, MODULE ");
            SQL.Append("\nFROM UserAssignments");

            DataTable dt = null;
            DataRow[] rows = null;
            dt = _con.ExecuteDataTable(SQL.ToString());
            //  int res;
            if (dt.Rows.Count > 0)
            {
                #region IPT Security Rights...

                rows = dt.Select("RightsName = 'UserRights'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("clwsf< k|Tofof]hg", "UserRights");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'DataBackup'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("tYof_s ;_rog", "DataBackup");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'DataRestore'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("tYof_s k''gn]{vg", "DataRestore");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'MotherLanguage'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("dft[efiff", "MotherLanguage");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'Religion'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("wd{", "Religion");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'Occupation'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("k]zf", "Occupation");
                }

                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'ConstructionType'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";_<rgfsf lsl;dx?", "ConstructionType");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'ConstructionUsed'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";_<rgfsf] k|of]u", "ConstructionUsed");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'HouseConstructionType'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("#<sf] agfj^", "HouseConstructionType");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'HouseType'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("#<sf] lsl;d", "HouseType");
                }

                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'AquisitionType'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";DklQ k|flKtsf lsl;d", "AquisitionType");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'ExclusionReason'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("d" + @"Nofª\\sgdf ;dfj]z gx''g] sf<)f", "ExclusionReason");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'RoadRelatedEntryList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";*s;Fu ;DalGwt ljj<)f", "RoadRelatedEntryList");
                }

                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'LandConstructionLocationList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("hUuf tyf ;_<rgfx?sf] cjl:ylt", "LandConstructionLocationList");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'OldVdcList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("k''<fgf uf.lj.;. x?", "OldVdcList");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'PresentVDClList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("xfnsf uf.lj.;. x?", "PresentVDClList");
                }

                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'DifferentChangableIndicator'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("kl<jt{gLo ;''rs", "DifferentChangableIndicator");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'IncomeBillRelatedEntryList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("cfon]vf ;DaGwL ljj<)f", "IncomeBillRelatedEntryList");
                }

                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'TaxPayersIndvList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("s<bftfx?sf] JolQmut ljj<)f", "TaxPayersIndvList");
                }
                //next rights
                rows = null;
                rows = dt.Select("RightsName = 'TaxPayersLists'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";DklQ ;DaGwL ljj<)f", "TaxPayersLists");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'SelectionOfPropertyForEvaluationRate'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";DklQ d" + @"NofÍgsf b<-<]^", "SelectionOfPropertyForEvaluationRate");
                }
                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'PropertyRelatedEntry'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";DklQ d" + @"Nofªsg tyf s< lgwf{<)f", "PropertyRelatedEntry");
                }
                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'BusinessTaxRelatedEntryList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("Joj;fo s< Joj:yfkg", "BusinessTaxRelatedEntryList");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'VehiclTaxRelatedEntryList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";jf<L ;fwg s< Joj:yfkg", "VehiclTaxRelatedEntryList");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'AccordingtoTaxPearReport'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("JolQmut tyf ;DklQs< k|ltj]gx?", "AccordingtoTaxPearReport");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'PrpoertyEvalRate'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";DklQ d" + @"Nofª\sgsf b<<]^", "PrpoertyEvalRate");
                }
                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'BusinessTaxReport'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("Joj;fo s<;Fu ;DalGwt k|ltj]gx?", "BusinessTaxReport");
                }
                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'VehicleTaxReport'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";jf<L s<;Fu ;DjlGwt k|ltj]bgx?", "VehicleTaxReport");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'CollectionCenterReports'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights(";__sngs]Gb|n] a''emfpg''kg]{ k|ltj]bgx?", "CollectionCenterReports");
                }

                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'UserWorkSummaryList'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("k|of]ustf{x?n] u<]sf] k|ljli^x?sf] ;f<f_z", "UserWorkSummaryList");
                }
                //next set rights name
                rows = null;
                rows = dt.Select("RightsName = 'MunicipilityLandArea'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("j*f cg'';f< hUufsf] If]qkmn", "MunicipilityLandArea");
                }

                #endregion IPT Security Rights...

                #region PMIS Security Rights

                rows = null;
                rows = dt.Select("RightsName = 'DataBackup'");
                if (rows.Length < 1)
                {
                    Status = InSertSecurityRights("tYof_s ;_rog", "DataBackup");
                }

                #endregion PMIS Security Rights

                //last rights name add later other rights name if require
                return Status;
            }
            return false;
        }

        private bool InSertSecurityRights(string NepName, string RightName)
        {
            if (NepName.Length == 0)
                throw new Exception("Nepali Name can't be blank.");
            if (RightName.Length == 0)
                throw new Exception("Rights Name can'tbe blank.");

            StringBuilder SQL = new StringBuilder();
            SQL.Remove(0, SQL.Length);
            SQL.Append("INSERT INTO UserAssignments");
            SQL.Append("\n(SN, NepName, RightsName)");
            SQL.Append("\n SELECT USERASSIGNMENTS_SEQ.NEXTVAL");
            SQL.AppendFormat(",'{0}'", NepName.Replace("'", "''").ToString());
            SQL.AppendFormat(",'{0}'", RightName.ToString());
            SQL.Append(" FROM dual");

            int res = _con.ExecuteNonQuery(SQL.ToString());
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public bool DefaultAdminUserCreate()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append("\n SN, Userid");
            sql.Append("\nFROM USERMASTER");
            sql.Append("\n WHERE");
            sql.AppendFormat("\n UserID = '{0}'", "ADMIN");
            DataTable rs = null;

            rs = _con.ExecuteDataTable(sql.ToString());
            if (rs.Rows.Count < 1)
            {
                sql.Remove(0, sql.Length);
                sql.Append("INSERT INTO USERMASTER");
                sql.Append("( SN, UserID, USERPASSWORD, NepName, UPDATEDBY )");
                sql.Append("\n SELECT ");
                sql.AppendFormat("\n {0}", "USERMASTER_SEQ.NEXTVAL");
                sql.AppendFormat("\n , '{0}'", "ADMIN");
                sql.AppendFormat("\n , '{0}'", "");
                sql.AppendFormat("\n , '{0}'", "<fh:j k|d'v".Replace("'", "''"));
                sql.AppendFormat("\n, {0}", " 1");
                sql.Append(" FROM dual");
                int res = _con.ExecuteNonQuery(sql.ToString());
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// this method working now only varchar data type not user the Numeric
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ColsName"></param>
        /// <param name="DataTypes"></param>
        /// <param name="NewDataLenght"></param>
        /// <returns></returns>

        public bool ChangeColumnDataLength(string tableName, string ColsName, string DataTypes, int NewDataLenght)
        {
            string tblName = tableName.Trim().ToUpper().ToString();
            string colName = ColsName.Trim().ToUpper().ToString();
            string dbType = DataTypes.Trim().ToUpper().ToString();
            int NewdbLength = NewDataLenght;

            if (tblName.Length == 0)
                throw new Exception("tableName must be passed");

            if (colName.Length == 0)
                throw new Exception("columnName must be passed");

            if (dbType.Length == 0)
                throw new Exception("dataType must be passed");
            if (NewdbLength == 0)
                throw new Exception("New Data Lenght muse be passed");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_tab_columns ");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t table_name = '{0}'", tblName);
            sql.AppendFormat("\n\t AND column_name = '{0}'", colName.Trim().ToUpper());
            sql.AppendFormat("\n AND DATA_TYPE = '{0}'", dbType);
            sql.AppendFormat("\n AND DATA_LENGTH = '{0}'", NewdbLength);

            DataTable rsTemp = null;
            rsTemp = _con.ExecuteDataTable(sql.ToString());
            if (rsTemp.Rows.Count < 1)
            {
                sql.Remove(0, sql.Length);
                sql.Append("ALTER TABLE");
                sql.AppendFormat("\n {0}", tblName);
                sql.Append("\nMODIFY ");
                sql.AppendFormat("\n {0}", colName);
                sql.AppendFormat("\n {0}", dbType);
                sql.AppendFormat(" ({0})", NewdbLength);

                int res = _con.ExecuteNonQuery(sql.ToString());
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Mustly Wokr Data Type Number
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ColsName"></param>
        /// <param name="DataTypes"></param>
        /// <param name="DataLenght"></param>
        /// <returns></returns> //numeric data types..
        public bool ColumnDataLengthExists(string tableName, string ColsName, string DataTypes, int DataLenght)
        {
            string tblName = tableName.Trim().ToUpper().ToString();
            string colName = ColsName.Trim().ToUpper().ToString();
            string dbType = DataTypes.Trim().ToUpper().ToString();

            if (tblName.Length == 0)
                throw new Exception("tableName must be passed");

            if (colName.Length == 0)
                throw new Exception("columnName must be passed");

            if (dbType.Length == 0)
                throw new Exception("dataType must be passed");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_tab_columns ");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t table_name = '{0}'", tblName);
            sql.AppendFormat("\n\t AND column_name = '{0}'", colName.Trim().ToUpper());
            sql.AppendFormat("\n AND DATA_TYPE = '{0}'", dbType);
            sql.AppendFormat("\n AND DATA_PRECISION = '{0}'", DataLenght);

            DataTable rsTemp = null;
            rsTemp = _con.ExecuteDataTable(sql.ToString());
            if (rsTemp.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        //checking Varchar data lenght
        public bool ColumnVarcharDataLengthExists(string tableName, string ColsName, string DataTypes, int DataLenght)
        {
            string tblName = tableName.Trim().ToUpper().ToString();
            string colName = ColsName.Trim().ToUpper().ToString();
            string dbType = DataTypes.Trim().ToUpper().ToString();

            if (tblName.Length == 0)
                throw new Exception("tableName must be passed");

            if (colName.Length == 0)
                throw new Exception("columnName must be passed");

            if (dbType.Length == 0)
                throw new Exception("dataType must be passed");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_tab_columns ");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t table_name = '{0}'", tblName);
            sql.AppendFormat("\n\t AND column_name = '{0}'", colName.Trim().ToUpper());
            sql.AppendFormat("\n AND DATA_TYPE = '{0}'", dbType);
            sql.AppendFormat("\n AND DATA_LENGTH = '{0}'", DataLenght);

            DataTable rsTemp = null;
            rsTemp = _con.ExecuteDataTable(sql.ToString());
            if (rsTemp.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool ChangeColumnDataType(string tableName, string ColName, string OldDataTyes, string NewDataTypes, string dataLenght)
        {
            string _tblName = tableName.Trim().ToUpper().ToString();
            string _colName = ColName.Trim().ToUpper().ToString();
            string _olddbType = OldDataTyes.Trim().ToUpper().ToString();
            string _NewdbType = NewDataTypes.Trim().ToUpper();

            if (_tblName.Length == 0)
                throw new Exception("tableName must be passed");

            if (_colName.Length == 0)
                throw new Exception("columnName must be passed");

            if (_olddbType.Length == 0)
                throw new Exception("dataType must be passed");
            if (_NewdbType.Length == 0)
                throw new Exception("New Data Type muse be passed");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT 1 ");
            sql.Append("\nFROM user_tab_columns ");
            sql.Append("\nWHERE ");
            sql.AppendFormat("\n\t table_name = '{0}'", _tblName);
            sql.AppendFormat("\n\t AND column_name = '{0}'", _colName.Trim().ToUpper());
            sql.AppendFormat("\n AND DATA_TYPE = '{0}'", _NewdbType);
            ////if (dataLenght.Trim() != "")
            ////{
            ////    sql.AppendFormat(" {0}", dataLenght);
            ////}

            DataTable rsTemp = null;
            rsTemp = _con.ExecuteDataTable(sql.ToString());
            if (rsTemp.Rows.Count < 1)
            {
                sql.Remove(0, sql.Length);
                sql.Append("ALTER TABLE");
                sql.AppendFormat("\n {0}", _tblName);
                sql.Append("\nMODIFY ");
                sql.AppendFormat("\n {0}", _colName);
                sql.AppendFormat("\n {0}", _NewdbType);
                if (dataLenght.Trim() != "")
                {
                    sql.AppendFormat(" {0}", dataLenght);
                }
                //sql.AppendFormat(" ({0})", NewdbLength);

                int res = _con.ExecuteNonQuery(sql.ToString());
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetServerDate()
        {
            StringBuilder Sql = new StringBuilder();
            //SELECT GETDATE() for unformated
            Sql.Append("select CONVERT(nvarchar(30), GETDATE(), 103)");
            //  Sql.Append("SELECT To_Char(SYSDATE,'dd-Mon-yy') dt FROM DUAL");

            DataTable rsT = null;
            rsT = _con.ExecuteDataTable(Sql.ToString());
            if (rsT.Rows.Count > 0)
            {
                _SrvDate = string.Empty;
                _SrvDate = rsT.Rows[0][0].ToString();
                return true;
            }
            return false;
        }

        #endregion Methods

        #region IAS releate unused column Drop

        /// <summary>
        /// this methods colum drop only None relation colums
        /// Drop Unused columns
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public bool DropColumnUnused(string tblName, string ColumnName)
        {
            if (tblName.Length < 1)
                throw new Exception("Must pass the table Name");
            if (ColumnName.Length < 1)
                throw new Exception("Must pass the Column Name");
            StringBuilder SqlDel = new StringBuilder();
            string TablesNames = tblName.ToUpper().Trim();
            string ColumnsNames = ColumnName.ToUpper().Trim();

            //First Checking Given Column Constraint exists or not=====

            SqlDel.Append("SELECT CONSTRAINT_NAME ");
            SqlDel.Append("\n FROM user_cons_columns ");
            SqlDel.Append("\n WHERE");
            SqlDel.AppendFormat("\n TABLE_NAME = '{0}'", TablesNames);
            SqlDel.AppendFormat("\n AND COLUMN_NAME = '{0}'", ColumnsNames);

            DataTable dt = null;
            dt = _con.ExecuteDataTable(SqlDel.ToString());
            if (dt.Rows.Count > 0)
            {
                //droping Constrant if exists
                SqlDel.Remove(0, SqlDel.Length);
                SqlDel.Append("ALTER TABLE ");
                SqlDel.AppendFormat(" {0}  DROP constraint", TablesNames);
                SqlDel.AppendFormat(" {0}", dt.Rows[0]["CONSTRAINT_NAME"].ToString());

                int del = _con.ExecuteNonQuery(SqlDel.ToString());
            }

            //==================================

            SqlDel.Remove(0, SqlDel.Length);

            SqlDel.Append("ALTER TABLE ");
            SqlDel.AppendFormat(" {0}", tblName);
            SqlDel.Append("\n DROP COLUMN ");
            SqlDel.AppendFormat(" {0}", ColumnName);

            int rslt = _con.ExecuteNonQuery(SqlDel.ToString());
            if (rslt > 0)
            {
                //Success..
                LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, string.Format("(0) table {0} columns drop successfully.", TablesNames, ColumnsNames));

                return true;
            }
            return false;
        }

        #endregion IAS releate unused column Drop
    }

  

  
}
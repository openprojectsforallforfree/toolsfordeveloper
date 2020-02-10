using System;
using System.Data;
using System.Windows.Forms;

namespace Bsoft.Data
{
    public class Validation
    {
        /// <summary>
        /// </summary>
        /// <param name="val">STRING VALUE THAT HAS TO BE CHECKED.</param>
        /// <param name="NumberStyle">NUMBER STYLE</param>
        /// <returns></returns>
        public static bool IsNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            bool retVal = false;
            try
            {
                retVal = Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
            }
            catch
            {
                //
            }
            return retVal;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val">STRING VALUE THAT HAS TO BE CHECKED.</param>
        /// <param name="NumberStyle">NUMBER STYLE</param>
        /// <returns></returns>
        public static bool IsNumeric(string val)
        {
            Double result;
            bool retVal = false;
            try
            {
                retVal = Double.TryParse(val, out result);
            }
            catch
            {
                //
            }
            return retVal;
        }

        /// <summary>
        /// Check if the variable has Null value or not.
        /// </summary>
        /// <param name="Value">The value of which DBNull is being checkd</param>
        /// <remarks>Boolean value specifying whether the passed value is Null or not.</returns>
        public static bool IsDBNull(object Value)
        {
            if (Value == null)
                return true;

            if (Value is System.DBNull)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the variable has Null value or not . If the passed value is DB NULL type then returns a default value
        /// </summary>
        /// <param name="Value">The value of which DBNull is being checkd</param>
        /// <param name="DataType">DataType for returing the default value if null exists</param>
        /// <returns></returns>
        public static object IsDBNull(object Value, DbType DataType)
        {
            if (Value is System.DBNull)
            {
                switch (DataType)
                {
                    case DbType.String:
                        Value = "";
                        break;

                    case DbType.Int32:
                    case DbType.Int16:
                    case DbType.Int64:
                    case DbType.UInt16:
                    case DbType.UInt32:
                    case DbType.UInt64:
                    case DbType.Byte:
                    case DbType.Currency:
                    case DbType.Decimal:
                    case DbType.Double:
                        Value = 0;
                        break;

                    case DbType.Boolean:
                        Value = false;
                        break;

                    case DbType.Date:
                    case DbType.DateTime:
                        Value = "";
                        break;
                }
            }
            return Value;
        }

        public static bool CheckIpAddress(string ipAddress)
        {
            bool Status = true;

            // IF THERE IS NO IP ADDRESS THEN IT IS SUPPOSE TO LOCALHOST.
            if (ipAddress.Trim().Length == 0)
            {
                return Status;
            }

            // SPLITING THE IP ADDRESS FOR VALIDATION.
            string[] splitIp = ipAddress.Split('.');

            // CHECKING THE LENGTH OF IP ADDRESS.
            // THE DEFAULT LENGHT IS 4. AS 255.255.255.255
            if (splitIp.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    // CHECKING EACH IP ADDRESS.
                    if (Convert.ToInt32(splitIp[i]) > 255 || Convert.ToInt32(splitIp[i]) < 0)
                    {
                        Status = false;
                        break;
                    }
                }
            }
            else
            {
                Status = false;
            }

            if (Status == false)
                MessageBox.Show("Invalid Ip Address");

            return Status;
        }

        /// <summary>
        /// supports FORMATS ddmmyy ddmmyyyy yy-mm-dd etc
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <Author>Dhiraj Bajracharya</Author>
        public static bool IsValidGregorianDate(string value, string format)
        {
            format = format.ToLower();
            if (value.Length != format.Length)
            {
                return false;
            }
            try
            {
                if (value.Length < 6)
                {
                    return false;
                }
                int monthindex = format.IndexOf("mm");
                int dayindex = format.IndexOf("dd");
                int dayVal = Convert.ToInt16(value.Substring(dayindex, 2));
                int monthVal = Convert.ToInt16(value.Substring(monthindex, 2));

                int yearindex = format.IndexOf("yyyy");
                int yearVal = 0;
                if (yearindex < 0)
                {
                    if (format.IndexOf("yyy") > -1)
                    {
                        throw new Exception("Invalid format,format should contain ddd AND (yy OR yyyy)");
                    }
                    yearindex = format.IndexOf("yy");
                    yearVal = Convert.ToInt16(value.Substring(yearindex, 2));
                    yearVal += 2000;
                }
                else
                {
                    yearVal = Convert.ToInt16(value.Substring(yearindex, 4));
                }
                if (yearindex < 0 || monthindex < 0 || dayindex < 0)
                {
                    throw new Exception("Invalid format,format should contain dd,mm AND (yy OR yyyy)");
                }
                DateTime dt = new DateTime(yearVal, monthVal, dayVal);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// supports FORMATS ddmmyy ddmmyyyy yy-mm-dd etc
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <Author>Dhiraj Bajracharya</Author>
        public static DateTime GetGregorianDate(string value, string format)
        {
            format = format.ToLower();
            if (!IsValidGregorianDate(value, format))
            {
                throw new Exception("Invalid date first check validity");
            }
            int monthindex = format.IndexOf("mm");
            int dayindex = format.IndexOf("dd");
            int dayVal = Convert.ToInt16(value.Substring(dayindex, 2));
            int monthVal = Convert.ToInt16(value.Substring(monthindex, 2));

            int yearindex = format.IndexOf("yyyy");
            int yearVal = 0;
            if (yearindex < 0)
            {
                if (format.IndexOf("yyy") > -1)
                {
                    throw new Exception("Invalid format,format should contain ddd AND (yy OR yyyy)");
                }
                yearindex = format.IndexOf("yy");
                yearVal = Convert.ToInt16(value.Substring(yearindex, 2));
                yearVal += 2000;
            }
            else
            {
                yearVal = Convert.ToInt16(value.Substring(yearindex, 4));
            }

            DateTime dt = new DateTime(yearVal, monthVal, dayVal);
            return dt;
        }

        //public static bool IsDateTime(string Val)
        //{
        //    if (Val .Length != Configuration.DB_DATE_FORMAT .Length )
        //    {
        //        return false;
        //    }

        //    DateTime dt = new DateTime(Val.Substring ( );
        //    try
        //    {
        //        return DateTime.TryParse(Val, out dt);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
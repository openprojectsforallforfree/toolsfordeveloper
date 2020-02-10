using System;
using System.Data;
using System.Windows.Forms;

namespace Bsoft.Common
{
    public static class Conversion
    {
        public static bool ToBoolean(this string value)
        {
            bool result = false;
            try
            {
                bool.TryParse(value, out result);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool ToBoolean(DataRow dRow, string columnName)
        {
            CheckIfColumnExists(dRow, columnName);
            bool result = false;
            try
            {
                bool.TryParse(dRow[columnName].ToString(), out result);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        //public static decimal ToDecimal(string value)
        //{
        //    if (value == "")
        //    {
        //        return 0;

        //    }
        //    else
        //    { return Convert.ToDecimal(value); }
        //}

        public static decimal ToDecimal(this string value)
        {
            decimal result = 0M;
            try
            {
                decimal.TryParse(value, out result);
            }
            catch
            {
                result = 0M;
            }
            return result;
        }

        public static decimal ToDecimal(DataRow dRow, string columnName)
        {
            CheckIfColumnExists(dRow, columnName);
            decimal result = 0M;
            try
            {
                decimal.TryParse(dRow[columnName].ToString(), out result);
            }
            catch
            {
                result = 0M;
            }
            return result;
        }

        private static void CheckIfColumnExists(DataRow dRow, string columnName)
        {
            if (dRow.Table.Columns.IndexOf(columnName) == -1)
            {
                throw new Exception("Column Name doesnot exists.");
            }
        }

        /// <summary>
        /// returns 0 for invalid input
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double ToDoubleAbsolute(this string val)
        {
            try
            {
                return double.Parse(val);
            }
            catch
            {
                return 0;//Negative means not valid input string
            }
        }

        //public static decimal ToDecimal(DataRow dRow, string columnName)
        //{
        //    try
        //    {
        //        return Convert.ToDecimal(dRow[columnName]);
        //    }
        //    catch { return 0; }
        //}

        public static decimal ToDecimal(DataGridViewRow dRow, string columnName)
        {
            try
            {
                return Convert.ToDecimal(dRow.Cells[columnName].Value);
            }
            catch
            { return 0; }
        }

        public static double ToDouble(this object value)
        {
            try
            {
                return Convert.ToDouble(value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(DataRow dRow, string columnName)
        {
            return Convert.ToDouble(dRow[columnName]);
        }

        public static int ToInt32(object value)
        {
            try
            {
                return Convert.ToInt32(value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        public static int ToInt32(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return -1;
            }
        }

        public static int ToInt32(DataRow dRow, string columnName)
        {
            try
            {
                return Convert.ToInt32(dRow[columnName]);
            }
            catch
            {
                return -1;
            }
        }

        public static string ToString(DataRow dRow, string columnName)
        {
            string RETVAL;
            try
            {
                RETVAL = dRow[columnName].ToString();
            }
            catch
            {
                RETVAL = "";
            }

            return RETVAL;
        }

        public static string ToString(object obj)
        {
            string RETVAL;
            try
            {
                RETVAL = obj.ToString();
            }
            catch
            {
                RETVAL = "";
            }

            return RETVAL;
        }

        public static string ToString(DataGridViewRow dgvr, string columnName)
        {
            string RETVAL;
            try
            {
                RETVAL = dgvr.Cells[columnName].Value.ToString();
            }
            catch
            {
                RETVAL = "";
            }
            return RETVAL;
        }

        public static double ToDouble(DataGridViewRow dgvr, string columnName)
        {
            double RETVAL;
            try
            {
                RETVAL = Convert.ToDouble(ToString(dgvr, columnName));
            }
            catch
            {
                RETVAL = -1;
            }
            return RETVAL;
        }

        #region Utility

        /// <summary>
        /// Returns 0 for non numbers and blanks
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this object val)
        {
            try
            {
                if (val.ToString().Contains("."))
                {
                    return int.Parse(val.ToString().Substring(0, val.ToString().IndexOf(".")));
                }
                else
                    return int.Parse(val.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static long ToLong(this string val)
        {
            try
            {
                if (val.ToString().Contains("."))
                {
                    return long.Parse(val.ToString().Substring(0, val.ToString().IndexOf(".")));
                }
                else
                    return long.Parse(val.ToString());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns 0 for non numbers and blanks
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        //public static double ToDouble(this string val)
        //{
        //    try
        //    {
        //        return double.Parse(val);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        #endregion Utility
    }
}
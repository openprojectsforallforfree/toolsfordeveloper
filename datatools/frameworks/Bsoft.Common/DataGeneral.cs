using System.Collections;

using System.Data;

//using Oracle.DataAccess.Client;

namespace Bsoft.Data
{
    /// <summary>
    /// Class for holding all the general data related functions.
    /// </summary>
    public static class DataGeneral
    {
        #region "object disposing"

        public static void DisposeObject(DataTable dt)
        {
            if ((dt != null))
            {
                if (dt.Rows != null)
                    dt.Rows.Clear();
                dt.Clear();
                dt.Dispose();
                dt = null;
            }
        }

        public static void DisposeObject(ref byte[] arr)
        {
            if ((arr != null))
            {
                System.Array.Clear(arr, 0, arr.Length);
                arr = null;
            }
        }

        public static void DisposeObject(ref ArrayList arrList)
        {
            if ((arrList != null))
            {
                arrList.Clear();
                arrList = null;
            }
        }

        public static void DisposeObject(ref object[] objArr)
        {
            if ((objArr != null))
            {
                //objArr.Clear(objArr, 0, objArr.Length);
                objArr = null;
            }
        }

        #endregion "object disposing"

        /// <summary>
        /// for sqlite only
        /// </summary>
        /// <param name="constring"></param>
        /// <returns></returns>
        public static string GetPathFromConnectionstring(string constring)
        {
            //eg foreign keys=True; Data Source=resto;

            constring = constring.Replace(" ", "").ToLower();
            constring = constring + ";";
            int start = constring.LastIndexOf("datasource=") + "datasource=".Length;
            int end = constring.IndexOf(";", start);
            return constring.Substring(start, end - start);
        }
    }
}
using System;

namespace Bsoft.Common
{
    public static class DateUtility
    {
        public const string DbDateFormat = "yyyyMMdd";
        public const string DisplayDateFormat = "dd-MMM-yyyy";

        /// <summary>
        ///
        /// </summary>
        /// <param name="date">In format yyyyMMdd</param>
        /// <param name="format">output format for date </param>
        /// <returns></returns>
        public static string GetFormatedDate(object date, string format)
        {
            return GetDate(date.ToString()).ToString(format);
        }

        public static DateTime GetDate(object date)
        {
            int y = date.ToString().Substring(0, 4).ToInt();
            int m = date.ToString().Substring(4, 2).ToInt();
            int d = date.ToString().Substring(6, 2).ToInt();
            return new DateTime(y, m, d);
        }

        public static TimeSpan DateDiff(object fromDate, object toDate)
        {
            return GetDate(fromDate.ToString()) - GetDate(toDate.ToString());
        }
    }
}
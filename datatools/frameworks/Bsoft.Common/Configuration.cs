namespace Friuts
{
    public static class Configuration
    {
        #region Date Format Constants.

        public const string DB_DATE_FORMAT = "yyyyMMdd";
        public const string DB_DATETIME_FORMAT = "dd-MMM-yyyy HH:mm:ss";
        public const string DB_DATETIME_FORMAT_STRING = "DD-MON-yyyy HH24:MI:ss";

        public const string DATE_DISPLAY_FORMAT = "yyyy/MM/dd";
        public const string DATETIME_FORMAT = "yyyy/MM/dd HH:mm";
        public const string DISPLAY_DATE_FORMAT = "dd-MMM-yyyy";

        #endregion Date Format Constants.

        public static bool UseRoundedValue = false;
    }
}
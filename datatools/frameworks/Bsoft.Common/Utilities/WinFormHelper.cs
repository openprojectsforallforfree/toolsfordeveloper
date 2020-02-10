using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Friuts
{
    public enum ThousandSeparaterTypes
    {
        NONE = 0,
        English = 1,
        Nepali = 2,
    }

    public enum ColumnTypes
    {
        Guid,
        String,
        Number,
        Boolean,
        EnglishDate,
        EnglishDateTime,
        NepaliDate,

        /// <summary>
        /// Formats value to integer.
        /// </summary>
        Integer,

        /// <summary>
        /// Formats values to integer with thousand separater and rounding as per organisationl setting.
        /// </summary>
        Amount,

        /// <summary>
        /// Formats values to integer with thousand separater and rounding will be done to 2 decimal palces
        /// </summary>
        AmountRound2,

        /// <summary>
        /// Formats values to integer with thousand separater and rounding will be done to 0 decimal palces
        /// </summary>
        AmountRound,

        /// <summary>
        ///
        /// </summary>
        SysDate
    }

    public static class WinFormHelper
    {
        #region Member variables.

        #endregion Member variables.

        #region Constructors & Finalizers.

        #endregion Constructors & Finalizers.

        #region Nested Enums, Structs, and Classes.

        #endregion Nested Enums, Structs, and Classes.

        #region Properties

        #endregion Properties

        #region Methods

        public static string GetFormattedNumber(string value, ThousandSeparaterTypes separaterType)
        {
            return GetFormattedNumber(value, separaterType, 0);
        }

        public static string GetFormattedNumber(string value, ThousandSeparaterTypes separaterType, int decimalPlaces)
        {
            bool isNegetive = false;

            string trailingFormat = string.Empty;
            if (decimalPlaces > 0)
                trailingFormat = "0.".PadRight(decimalPlaces + 2, '0');
            else
                trailingFormat = "0";

            switch (separaterType)
            {
                case ThousandSeparaterTypes.English:
                    value = double.Parse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint |
                                             NumberStyles.AllowLeadingSign).ToString("###,###,##" + trailingFormat);
                    break;

                case ThousandSeparaterTypes.Nepali:
                    if (value.StartsWith("-"))
                        isNegetive = true;
                    value = value.Replace("-", string.Empty);

                    value = double.Parse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint |
                        NumberStyles.AllowLeadingSign).ToString("##~##~##~##~##~##~##" + trailingFormat).Replace("~", ",").Trim(',');

                    if (isNegetive)
                        value = "-" + value;
                    break;

                case ThousandSeparaterTypes.NONE:
                    value = double.Parse(value, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint |
                        NumberStyles.AllowLeadingSign).ToString().Replace(",", string.Empty);
                    break;
            }

            return value;
        }

        public static Font ChangeFontBoldness(Font font, bool isBold)
        {
            font = new Font(font, isBold ? FontStyle.Bold : FontStyle.Regular);
            return font;
        }

        public static void ChangeFontBoldness(CheckBox ctrl)
        {
            bool isBold = ctrl.Checked;
            ctrl.Font = new Font(ctrl.Font, isBold ? FontStyle.Bold : FontStyle.Regular);
            //return font;
        }

        public static void ChangeFontBoldRadioButton(RadioButton Opt)
        {
            bool isBold = Opt.Checked;
            Opt.Font = new Font(Opt.Font, isBold ? FontStyle.Bold : FontStyle.Regular);
        }

        #endregion Methods
    }
}
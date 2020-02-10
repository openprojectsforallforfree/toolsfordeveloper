using System;
using System.Text;
using System.Windows.Forms;

namespace Bsoft.Common.Language
{
    //asumes only 2 languages in play
    public static class LanguageUtility
    {
        public static string getinstalledLanguages()
        {
            StringBuilder sb = new StringBuilder();
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                sb.AppendLine(lang.Culture.EnglishName);
            }
            return sb.ToString();
        }

        public static string getCulture(string Language)
        {
            StringBuilder sb = new StringBuilder();
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().Contains(Language.ToLower().ToString()))
                {
                    return lang.Culture.CompareInfo.Name;
                }
            }
            return "";
        }

        public static string oldCulture = CultureLangs.EngCulture;

        public static void SetEng(this Control Ctrl)
        {
            Ctrl.Enter += new EventHandler(ctrl_EnterEng);
            //Ctrl.Leave += new EventHandler(Ctrl_Leave);
        }

        public static void SetNep(this Control Ctrl)
        {
            Ctrl.Enter += new EventHandler(ctrl_EnterOth);//Only change this
            // Ctrl.Leave += new EventHandler(Ctrl_Leave);
        }

        //only Add this
        private static void ctrl_EnterOth(object sender, EventArgs e)
        {
            oldCulture = InputLanguage.CurrentInputLanguage.Culture.Name;
            SetLang(CultureLangs.OthCulture);
        }

        private static void ctrl_EnterEng(object sender, EventArgs e)
        {
            oldCulture = InputLanguage.CurrentInputLanguage.Culture.Name;
            SetLang(CultureLangs.EngCulture);
        }

        //static void Ctrl_Leave(object sender, EventArgs e)
        //{
        //    SetLang(oldCulture);
        //}
        private static void SetLang(string cultureInfo)
        {
            System.Globalization.CultureInfo TypeOfLanguage = new System.Globalization.CultureInfo(cultureInfo);
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(TypeOfLanguage);
        }

        public static void SetEng()
        {
            SetLang(CultureLangs.EngCulture);
        }

        public static void SetNep()
        {
            SetLang(CultureLangs.OthCulture);
        }
    }

    public static class CultureLangs
    {
        public const string EngCulture = "en-us";
        public const string OthCulture = "ne-NP";
    }
}
using System;
using System.IO;
using System.Reflection;

namespace Bsoft.Common
{
    public class ResourceHelper
    {
        public static string RootNamespace = "RestoSys";

        public static string ReadString(string fileName)
        {
            try
            {
                Assembly _assembly;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(string.Format("{0}.{1}", RootNamespace, fileName)));
                string s = _textStreamReader.ReadToEnd();
                return s;
            }
            catch (Exception ex)
            {
                throw new Exception("ResourceHelperClass::Check root namespace or the file name.All are case sensitive.\n" + ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NMG.UI
{
    public static class FileHelper
    {

        public static string GetFullPath(string rootPath, string RelativePath)
        {
            if (RelativePath.StartsWith(@"\"))
            {
                RelativePath = RelativePath.Remove(0,1);
            }
            return Path.Combine(rootPath, RelativePath);
        }


        public static string GetRelativePath(string rootPath ,string FullPath)
        {
            return FullPath.Replace(rootPath, "");
        }

        public static string GetTemplateName(string path)
        {
            return path.Substring(path.LastIndexOf(@"\") + 1);
        }

    }
}

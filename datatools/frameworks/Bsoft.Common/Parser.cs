using System;
using System.Collections.Generic;

namespace Bsoft.Common
{
    public class ParserClass
    {
        private List<string> Info = new List<string>();

        public ParserClass(string s)
        {
            Populate(s);
        }

        public string GetValue(string key)
        {
            for (int i = 0; i < Info.Count; i++)
            {
                if (Info[i].ToLower().Trim() == key.ToLower().Trim())
                {
                    if (Info.Count > i + 1)
                    {
                        return Info[i + 1];
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// reads all
        /// comments start with --
        /// multiline starts with { and ends with }
        /// { and } lines are ignored
        /// </summary>
        /// <param name="pathss"></param>
        /// <param name="files"></param>
        private void PopulateWithDelimiter(string s)
        {
            string[] alllines = s.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            bool startAppend = false; string tempitem = "";
            foreach (string strline in alllines)
            {
                if (!strline.StartsWith("--"))
                {
                    if (strline.StartsWith("{"))
                    {
                        startAppend = true;
                        tempitem = Info[Info.Count - 1];
                        Info.RemoveAt(Info.Count - 1);
                    }
                    if (startAppend && strline.EndsWith("}"))
                    {
                        Info.Add(tempitem);
                        startAppend = false;
                    }
                    if (startAppend)
                    {
                        if (!strline.StartsWith("{"))
                        {
                            tempitem = tempitem + "\n" + strline;
                        }
                    }
                    if (!startAppend && !strline.EndsWith("}"))
                    {
                        Info.Add(strline);
                    }
                }
            }
        }

        //key
        //value
        //No deilmiter
        private void Populate(string s)
        {
            string[] alllines = s.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            bool startAppend = false;
            string tempitem = "";
            foreach (string strline in alllines)
            {
                if (!startAppend && strline.Trim().Length < 1)
                {
                    continue;
                }
                if (!strline.StartsWith("--"))
                {
                    if (strline.StartsWith("{"))
                    {
                        startAppend = true;
                        if (strline.Length > 1)
                        {
                            tempitem = strline.Substring(1);
                        }
                        else
                        {
                            tempitem = "";
                        }
                        continue;
                    }

                    if (strline.EndsWith("}"))
                    {
                        if (tempitem.EndsWith("}"))
                        {
                            if (tempitem.Length > 1)
                            {
                                tempitem = tempitem.Substring(0, tempitem.Length - 1);
                            }
                        }
                        else
                        {
                            if (strline.Length > 1)
                            {
                                tempitem = tempitem + System.Environment.NewLine + strline.Substring(0, strline.Length - 1);
                            }
                        }
                        Info.Add(tempitem);
                        startAppend = false;
                        continue;
                    }
                    if (startAppend)
                    {
                        if (tempitem.Trim().Length > 0)
                        {
                            tempitem = tempitem + System.Environment.NewLine + strline;
                        }
                        else
                        {
                            tempitem = strline;
                        }
                        continue;
                    }
                    if (!startAppend)
                    {
                        Info.Add(strline);
                    }
                }
            }
        }
    }
}
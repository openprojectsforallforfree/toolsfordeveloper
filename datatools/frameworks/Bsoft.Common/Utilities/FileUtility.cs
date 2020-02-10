﻿using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Bsoft.Common.Utilities
{
    internal class FileUtility
    { //remove illigal characters form filename etc
        public static string GetCleanPath(string fileName)
        {
            string regex = string.Format(
                   "[{0}]",
                   Regex.Escape(new string(Path.GetInvalidFileNameChars())));
            Regex removeInvalidChars = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            return removeInvalidChars.Replace(fileName, "_");
        }
    }

    public class UrlHelper
    {
        public static string Encode(string str)
        {
            var charClass = String.Format("0-9a-zA-Z{0}", Regex.Escape("-_.!~*'()"));
            return Regex.Replace(str,
                String.Format("[^{0}]", charClass),
                new MatchEvaluator(EncodeEvaluator));
        }

        public static string EncodeEvaluator(Match match)
        {
            return (match.Value == " ") ? "+" : String.Format("%{0:X2}", Convert.ToInt32(match.Value[0]));
        }

        public static string DecodeEvaluator(Match match)
        {
            return Convert.ToChar(int.Parse(match.Value.Substring(1), System.Globalization.NumberStyles.HexNumber)).ToString();
        }

        public static string Decode(string str)
        {
            return Regex.Replace(str.Replace('+', ' '), "%[0-9a-zA-Z][0-9a-zA-Z]", new MatchEvaluator(DecodeEvaluator));
        }
    }
}
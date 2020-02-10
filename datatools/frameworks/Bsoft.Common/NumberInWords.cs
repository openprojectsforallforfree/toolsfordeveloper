using System;

namespace Bsoft.Common
{
    public class NumberInWords
    {
        private string Number = string.Empty;
        private string deciml = string.Empty;
        private string _number = string.Empty;
        private string _deciml = string.Empty;
        private string[] US = new string[1003];
        private string[] SNu = new string[20];
        private string[] SNt = new string[10];

        public string inWords(string instr)
        {
            if (instr.IndexOf(".") < 0)
            {
                instr = instr + ".0";
            }
            int len = 0;
            if (instr.IndexOf(".") > 0)
            {
                len = instr.Substring(instr.IndexOf(".") + 1).Length;
                if (len < 2)
                {
                    instr = instr + "0";
                }
            }

            string outstr = string.Empty;

            string currency = "Rs. ";
            string _currency = " Paisa Only";
            if (Convert.ToDouble(instr) == 0)
            {
                outstr = "Zero Value";
                return "";
            }
            if (Convert.ToDouble(instr) < 0)
            {
                outstr = "Invalid Value";
                return "";
            }
            string[] no = instr.Split('.');
            if ((no[0] != null) && (no[1] != "00"))
            {
                Number = no[0];
                deciml = no[1];
                _number = (NameOfNumber(Number));
                _deciml = (NameOfNumber(deciml));
                if (_deciml.Trim() == "")
                {
                    outstr = currency + _number.Trim() + " Only";
                }
                else
                {
                    outstr = currency + _number.Trim() + " and " + _deciml.Trim() + _currency;
                }
            }
            if ((no[0] != null) && (no[1] == "00"))
            {
                Number = no[0];
                _number = (NameOfNumber(Number));
                outstr = currency + _number.Trim() + " Only";
            }
            if ((Convert.ToDouble(no[0]) == 0) && (no[1] != null))
            {
                deciml = no[1];
                _deciml = (NameOfNumber(deciml));
                outstr = _deciml + _currency;
            }
            return outstr;
        }

        private void Initialize()
        {
            SNu[0] = "";
            SNu[1] = "One";
            SNu[2] = "Two";
            SNu[3] = "Three";
            SNu[4] = "Four";
            SNu[5] = "Five";
            SNu[6] = "Six";
            SNu[7] = "Seven";
            SNu[8] = "Eight";
            SNu[9] = "Nine";
            SNu[10] = "Ten";
            SNu[11] = "Eleven";
            SNu[12] = "Twelve";
            SNu[13] = "Thirteen";
            SNu[14] = "Fourteen";
            SNu[15] = "Fifteen";
            SNu[16] = "Sixteen";
            SNu[17] = "Seventeen";
            SNu[18] = "Eighteen";
            SNu[19] = "Nineteen";
            SNt[2] = "Twenty";
            SNt[3] = "Thirty";
            SNt[4] = "Forty";
            SNt[5] = "Fifty";
            SNt[6] = "Sixty";
            SNt[7] = "Seventy";
            SNt[8] = "Eighty";
            SNt[9] = "Ninety";
            US[1] = "";
            US[2] = "Thousand";
            US[3] = "Million";
            US[4] = "Billion";
            US[5] = "Trillion";
            US[6] = "Quadrillion";
            US[7] = "Quintillion";
            US[8] = "Sextillion";
            US[9] = "Septillion";
            US[10] = "Octillion";
        }

        private string NameOfNumber(string Number)
        {
            Initialize();
            string GroupName = ""; string OutPut = "";
            if ((Number.Length % 3) != 0)
            {
                Number = Number.PadLeft((Number.Length + (3 - (Number.Length % 3))), '0');
            }
            string[] Array = new string[Number.Length / 3];
            Int16 Element = -1; Int32 DisplayCount = -1;
            bool LimitGroupsShowAll = false; int LimitGroups = 0;
            bool GroupToWords = true;
            for (Int16 Count = 0; Count <= Number.Length - 3; Count += 3)
            {
                Element += 1; Array[Element] = Number.Substring(Count, 3);
            }
            if (LimitGroups == 0)
            {
                LimitGroupsShowAll = true;
            }
            for (Int16 Count = 0; (Count <= ((Number.Length / 3) - 1)); Count++)
            {
                DisplayCount++; if (((DisplayCount < LimitGroups) || LimitGroupsShowAll))
                {
                    if (Array[Count] == "000") continue;
                    {
                        GroupName = US[((Number.Length / 3) - 1) - Count + 1];
                    }
                    if ((GroupToWords == true))
                    {
                        OutPut += Group(Array[Count]).TrimEnd(' ') + " " + GroupName + " ";
                    }
                    else { OutPut += Array[Count].TrimStart('0') + " " + GroupName; }
                }
            } Array = null; return OutPut;
        }

        private string Group(string Argument)
        {
            string Hyphen = "";
            string OutPut = "";
            Int16 d1 = Convert.ToInt16(Argument.Substring(0, 1));
            Int16 d2 = Convert.ToInt16(Argument.Substring(1, 1));
            Int16 d3 = Convert.ToInt16(Argument.Substring(2, 1));
            if ((d1 >= 1)) { OutPut += SNu[d1] + " hundred "; }
            if ((double.Parse(Argument.Substring(1, 2)) < 20))
            {
                OutPut += SNu[Convert.ToInt16(Argument.Substring(1, 2))];
            }
            if ((double.Parse(Argument.Substring(1, 2)) >= 20))
            {
                if (Convert.ToInt16(Argument.Substring(2, 1)) == 0)
                {
                    Hyphen += " ";
                }
                else
                {
                    Hyphen += " ";
                }
                OutPut += SNt[d2] + Hyphen + SNu[d3];
            }
            return OutPut;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string currency = "Rupees ";
        //    string _currency = " Paise Only";
        //    if (Convert.ToDouble(TextBox1.Text) == 0)
        //    {
        //        TextBox2.Text = "Null Value"; return;
        //    }
        //    if (Convert.ToDouble(TextBox1.Text) < 0)
        //    {
        //        TextBox2.Text = "Invalid Value";
        //        return;
        //    }
        //    string[] no = TextBox1.Text.Split('.');
        //    if ((no[0] != null) && (no[1] != "00"))
        //    {
        //        Number = no[0];
        //        deciml = no[1];
        //        _number = (NameOfNumber(Number));
        //        _deciml = (NameOfNumber(deciml));
        //        TextBox2.Text = currency + _number.Trim() + " and " + _deciml.Trim() + _currency;
        //    }
        //    if ((no[0] != null) && (no[1] == "00"))
        //    {
        //        Number = no[0];
        //        _number = (NameOfNumber(Number));
        //        TextBox2.Text = currency + _number + "Only";
        //    }
        //    if ((Convert.ToDouble(no[0]) == 0) && (no[1] != null))
        //    {
        //        deciml = no[1];
        //        _deciml = (NameOfNumber(deciml));
        //        TextBox2.Text = _deciml + _currency;
        //    }

        //}
    }
}
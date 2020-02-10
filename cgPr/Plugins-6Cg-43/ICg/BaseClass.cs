using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
//Help
//Uses Text for checkbox,radio button and label only
namespace ICg
{
    public class BasicFrame :  Frame
    {
        #region "declaration"
        int left_s = 0;
        int top_s = 0;
        protected StringBuilder outText = new StringBuilder();
        protected string tablename = "";
        protected string dbName = "";//1
        protected string type = "";//2
        protected string constraint = "";//3
        protected string txt = "";//4
        protected string cntrol = "";//5
        protected string controlName;//derived
        protected string crudeType;//derived
        protected string colName;//derived


        #endregion

        #region "Interface parts"
        public virtual void GenCreate_Afterlines()
        {
        }

        public virtual void GenCreate_Beforelines()
        {
        }

        public virtual void GenCreate_loop()
        {
        }

        public virtual void GenCreate_loop_Fistline()
        {
        }

        public virtual void GenCreate_loop_Lastline()
        {
        }
        #endregion

        #region "Publics"
        public string gencreate(int starting_left, int starting_top, DataTable dt)
        {
            left_s = starting_left;
            top_s = starting_top;
            return (gencreate(dt));
        }

        public virtual string gencreate(DataTable dt)
        {
            tablename = dt.TableName;
            int order = 0;
            GenCreate_Beforelines();
            foreach (DataRow dr in dt.Rows)
            {
                correctValues(dr["Table"].ToString(), dr["Type"].ToString(), dr["Constraint"].ToString(), dr["Text"].ToString(), dr["Control"].ToString());
                if (order == 0)
                {
                    GenCreate_loop_Fistline();
                }
                else if (order == dt.Rows.Count - 1)
                {
                    GenCreate_loop_Lastline();
                }
                else
                {
                    GenCreate_loop();
                }
                order = order + 1;
            }
            GenCreate_Afterlines();
            return outText.ToString();
        }

        #endregion

        #region "Correct values"

        public void correctValues(string _dbname, string _type, string _constraint, string _txt, string _control)
        {
            //find if empty
            dbName = _dbname == null ? string.Empty : _dbname;
            type = _type == null ? string.Empty : _type;
            constraint = _constraint == null ? string.Empty : _constraint;
            txt = _txt == null ? string.Empty : _txt;
            cntrol = _control == null ? string.Empty : _control;
            dbName = dbName.Replace(" ", "");
            //control 
            switch (cntrol)
            {
                case "lblComboBox":
                    controlName = "cmb" + dbName;
                    break;
                case "lblTextBox":
                    controlName = "txt" + dbName;
                    break;
                case "TextBox":
                    controlName = "txt" + dbName;
                    break;
                case "Label":
                    controlName = "lbl" + dbName;
                    break;
                case "ComboBox":
                    controlName = "cmb" + dbName;
                    break;
                case "CheckBox":
                    controlName = "chk" + dbName;
                    break;
                case "GroupBox":
                    controlName = "grp" + dbName;
                    break;
                case "Grid":
                    controlName = "col" + dbName;
                    break;
                case "RadioButton":
                    controlName = "rdo" + dbName;
                    break;
                case "DateTimePicker":
                    controlName = "dt" + dbName;
                    break;
                case "VtextBox":
                    controlName = "txt" + dbName;
                    break;
                default:
                    controlName = "oth" + dbName;
                    break;
            }
            //type
            crudeType = getCrudeType(type);
            //dbname
            colName = "col" + dbName;
        }

        public string getCrudeType(string Type)
        {
            if (Type.Contains("varchar"))
            {
                Type = "ColumnTypes.String";
            }
            if (Type.Contains("int"))
            {
                Type = "ColumnTypes.Number";
            }
            if (Type.Contains("number"))
            {
                Type = "ColumnTypes.Number";
            }
            if (Type.Contains("char"))
            {
                Type = "ColumnTypes.String";
            }
            if (Type.Contains("date"))
            {
                Type = "ColumnTypes.String";
            }
            if (Type.Contains("float"))
            {
                Type = "ColumnTypes.Number";
            }

            return Type;
        }

        #endregion

        #region "Functions"
        public bool isNumberType()
        {
            if (type.Contains("varchar"))
            {
                return false;
            }
            if (type.Contains("int"))
            {
                return true;
            }
            if (type.Contains("number"))
            {
                return true;
            }
            if (type.Contains("char"))
            {
                return false;
            }
            if (type.Contains("date"))
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Gives number from type eg float(6,3)
        /// </summary>
        /// <param name="instring"></param>
        /// <returns></returns>
        public int getNumber(string instring)
        {

            if (instring.ToLower() == "integer")
            {
                return 5;
            }

            string numString = "0";
            bool blnGotString = false;
            foreach (char c in instring)
            {
                if (Char.IsDigit(c))
                {
                    numString += c;
                    blnGotString = true;
                }
                else
                {
                    // to not to get the discontinuous numbers eg in float(8,2)
                    if (blnGotString == true)
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            int realNum = int.Parse(numString);
            return realNum;
        }

        #endregion

    }
}
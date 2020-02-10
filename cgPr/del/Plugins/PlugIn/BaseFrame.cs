using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
//Help
//Uses Text for checkbox,radio button and label only
namespace PlugIn
{
    public class BasicFrame : Base
    {

        #region "declaration"
        protected const string q = "\"";
        // " & q & "
        protected string tablename = "";
        protected  StringBuilder outText = new StringBuilder();

        int left_s = 0;
        int top_s = 0;
        protected string dbname = "";
        protected string type = "";
        protected string constraint = "";
        protected string txt = "";

        protected string Cntrol = "";
        protected string controlName;
        protected string CrudeType;

        protected string colName;
        protected string forceControl = "";

        string what;
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

        #region "Gencreate"
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
            dbname = _dbname == null ? string.Empty : _dbname;
            type = _type == null ? string.Empty : _type;
            constraint = _constraint == null ? string.Empty : _constraint;
            txt = _txt == null ? string.Empty : _txt;
            Cntrol = _control == null ? string.Empty : _control;
            dbname = dbname.Replace(" ", "");
            //control 
            if (forceControl != "NONE" & forceControl != null)
            {
                Cntrol = forceControl;
            }
            switch (Cntrol)
            {
                case "lblComboBox":
                    controlName = "cmb" + dbname;
                    break;
                case "lblTextBox":
                    controlName = "txt" + dbname;
                    break;
                case "TextBox":
                    controlName = "txt" + dbname;
                    break;
                case "Label":
                    controlName = "lbl" + dbname;
                    break;
                case "ComboBox":
                    controlName = "cmb" + dbname;
                    break;
                case "CheckBox":
                    controlName = "chk" + dbname;
                    break;
                case "GroupBox":
                    controlName = "grp" + dbname;
                    break;
                case "Grid":
                    controlName = "col" + dbname;
                    break;
                case "RadioButton":
                    controlName = "rdo" + dbname;
                    break;
                case "DateTimePicker":
                    controlName = "dt" + dbname;
                    break;
                case "VtextBox":
                    controlName = "txt" + dbname;
                    break;
                default:
                    controlName = "oth" + dbname;
                    break;
            }
            //type
            
            //dbname
            colName = "col" + dbname;
        }

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

        public string getCrudeType(string Type)
        {
            if (type.IndexOf("varchar") > 0)
            {
                Type = "ColumnTypes.String";
            }
            if (type.IndexOf("int") > 0)
            {
                Type = "ColumnTypes.Number";
            }
            if (type.IndexOf("number") > 0)
            {
                Type = "ColumnTypes.Number";
            }
            if (type.IndexOf("char") > 0)
            {
                Type = "ColumnTypes.String";
            }
            if (type.IndexOf("date") > 0)
            {
                Type = "ColumnTypes.String";
            }
            if (type.IndexOf("float") > 0)
            {
                Type = "ColumnTypes.Number";
            }

            return Type;
        }


        public bool isNumberType()
        {
            if (type.IndexOf("varchar") > 0)
            {
                return false;
            }
            if (type.IndexOf("int") > 0)
            {
                return true;
            }
            if (type.IndexOf("number") > 0)
            {
                return true;
            }
            if (type.IndexOf("char") > 0 )
            {
                return false;
            }
            if (type.IndexOf("date") > 0 )
            {
                return false;
            }
            return false;
        }


    }
}
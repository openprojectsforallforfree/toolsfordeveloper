using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ICg;

namespace myCg
{
    class myDbGen : PlugIn.MyClassBase
    {
        public myDbGen()
            : base()
        {
            Menu.Text = "DbGen";
        }
        public override void Execute()
        {
            ExcelExtension.GetValues egv = new ExcelExtension.GetValues();
            //Cg1.clsDb cd = new Cg1.clsDb();
            string textout = "";
            DataSet ds = egv.getValuesFromExcelExpand();
            if (ds == null)
            {
                return;
            }
            foreach (DataTable dt in ds.Tables)
            {
                textout = textout + "\n" + (new clsDb()).gencreate(dt);
            }
            Clipboard.SetText(textout);

        }
    }


    public class clsDb : BasicFrame, Frame
    {
        StringBuilder constraintString = new StringBuilder();

        public override void GenCreate_Afterlines()
        {
            //outText.AppendLine("Rslt = _dbStruct.Con.ExecuteNonQuery(changeSQL(SQLCreate.ToString()));");
            //outText.AppendLine("LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, \"" + tablename + " table created Successfully. \");");
            //outText.AppendLine("Status = true;}");
            outText.AppendLine("}");
        }
        public override void GenCreate_Beforelines()
        {

            outText.AppendLine("if (!GlobalResources.DbStruct.DoesTableExists(\"" + tablename + "\"))");
            outText.AppendLine("{");
            outText.AppendLine("createATable(@\" ");
            outText.AppendLine("CREATE TABLE " + tablename + "(");
            //outText.AppendLine("if (!_dbStruct.DoesTableExists( \"" + tablename + "\")){");
            //outText.AppendLine("SQLCreate.Remove(0, SQLCreate.Length);");
            //outText.AppendLine("SQLCreate.AppendLine( @\" CREATE TABLE " + tablename + "( \");");
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            if (constraint.ToLower().Trim () == "pk" | dbName.ToLower() == "id")
            {
                outText.AppendFormat("{0} {1} primary key autoincrement , ", dbName, type);//sqlite
               // outText.AppendFormat("{0} {1} primary key identity(1,1) , ", dbName, type);//sqlserver
            }
            else
            {
                outText.AppendFormat("{0} {1},", dbName, type);
                if (dbName.StartsWith ("fk") | dbName.ToLower().EndsWith("id"))
                {
                    constraintString.AppendFormat("CONSTRAINT FK_{1}_{0} FOREIGN KEY ({0}) REFERENCES {1} (Id), ", dbName, constraint);
                    constraintString.AppendLine();
                }
                
            }
            outText.AppendLine();
        }
        public override void GenCreate_loop_Fistline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            GenCreate_loop();

        }
        public override void GenCreate_loop_Lastline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            GenCreate_loop();
          
            string a = null;
            a = outText.ToString().Trim();
            a = a.Substring(0, a.Length - 1);
            string b = null;
            b ="," + constraintString.ToString().Trim();
            outText.Remove(0, outText.Length);
            outText.Append(a);
            if (table.type.ToLower() == "trackuser")
            {
                getTrackUserString();
            }
            try
            {
                outText.Append(b.Substring(0, b.Length - 2));
            }
            catch { }
            //outText = outText.Remove(outText.Length - 4, 4);
            outText.Append("))\");");
            outText.AppendLine();
        }

        private void getTrackUserString()
        {
            outText.AppendLine();
            outText.AppendFormat(",  {0} {1}, ", "UserIDEntry", "varchar");
            outText.AppendLine();
            outText.AppendFormat("  {0} {1}, ", "DT_DataEntry", "varchar");
            outText.AppendLine();
            outText.AppendFormat("  {0} {1}, ", "UserIDEdit", "varchar");
            outText.AppendLine();
            outText.AppendFormat("  {0} {1} ", "DT_DataEdit", "varchar");
            outText.AppendLine();
        }
    }


    public class clsDb2 : BasicFrame, Frame
    {
        StringBuilder constraintString = new StringBuilder();
        public override void GenCreate_Afterlines()
        {

        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine(" CREATE TABLE " + tablename + "( ");
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            //If constraint.ToLower = "pk" Or dbname.ToLower() = "id" Then
            //    outText.AppendFormat("{0} {1} primary key identity(1,1) ,", dbname, type)
            //Else
            //    If InStr(dbname, "fk") > 0 Or dbname.ToLower().EndsWith("id") Then

            //        constraintString.AppendFormat("  CONSTRAINT FK_{1}_{0} FOREIGN KEY ({0}) REFERENCES {1} (Id),", dbname, constraint)
            //        constraintString.AppendLine()
            //    End If
            //    outText.AppendFormat("{0} {1},", dbname, type)
            //End If
            outText.AppendFormat("{0} {1} {2},", dbName, type, constraint);
            outText.AppendLine();
        }
        public override void GenCreate_loop_Fistline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            GenCreate_loop();

        }
        public override void GenCreate_loop_Lastline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendFormat("{0} {1} {2}", dbName, type, constraint);
            outText.AppendLine(")");
        }
    }

}

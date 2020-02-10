using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using ICg;
namespace Cg1
{

    public class clsDb : BasicFrame, Frame
    {
        StringBuilder constraintString = new StringBuilder();

        public override void GenCreate_Afterlines()
        {
            outText.AppendLine("Rslt = _dbStruct.Con.ExecuteNonQuery(changeSQL(SQLCreate.ToString()));");
            outText.AppendLine("LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, \""   + tablename + " table created Successfully. \");");
            outText.AppendLine("Status = true;}");
        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine("if (!_dbStruct.DoesTableExists( \""   + tablename  + "\")){");
            outText.AppendLine("SQLCreate.Remove(0, SQLCreate.Length);");
            outText.AppendLine("SQLCreate.AppendLine( \" CREATE TABLE " + tablename + "( \");");
        }
        public override void GenCreate_loop()
        {
            if (constraint.ToLower() == "pk" | dbName.ToLower() == "id")
            {
                outText.AppendFormat("SQLCreate.AppendLine( \"{0} {1} primary key identity(1,1) ,\");", dbName, type);
            }
            else
            {

                if ( dbName.Contains ( "fk") | dbName.ToLower().EndsWith("id"))
                {
                    constraintString.AppendFormat("SQLCreate.AppendLine( \"  CONSTRAINT FK_{1}_{0} FOREIGN KEY ({0}) REFERENCES {1} (Id),\");", dbName, constraint);
                    constraintString.AppendLine();
                }
                outText.AppendFormat("SQLCreate.AppendLine( \" {0} {1},\");", dbName, type);

            }
            outText.AppendLine();
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();

        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();

            string a = null;
            a = outText.ToString().Trim();
            string b = null;
            b = constraintString.ToString().Trim();

            outText.Remove(0, outText.Length);
            outText.Append(a);
            outText.Append(b);
            outText = outText.Remove(outText.Length - 4, 4);
            outText.Append(")\");");
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
            GenCreate_loop();

        }
        public override void GenCreate_loop_Lastline()
        {
            outText.AppendFormat("{0} {1} {2}", dbName, type, constraint);
            outText.AppendLine(")");
        }
    }

}

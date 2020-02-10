using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ICg;
namespace myCg
{

    public class clsgridDeclare : BasicFrame, Frame
    {

        public clsgridDeclare()
        {
            forceControl = "Grid";
        }
        public override void GenCreate_Afterlines()
        {
        }

        public override void GenCreate_Beforelines()
        {
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            cntrol = "Grid";
            forceControl = "Grid";
            getcontrol("this");
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }


        public void getcontrol(string parent)
        {
            switch (cntrol)
            {

                case "Grid":

                    outText.AppendLine(" private System.Windows.Forms.DataGridViewTextBoxColumn  " + colName + "; ");

                    break;

            }
        }

    }

    public class clsGrid : BasicFrame, Frame
    {

        public clsGrid()
        {
            forceControl = "Grid";
        }
        public override void GenCreate_Afterlines()
        {
        }

        public override void GenCreate_Beforelines()
        {
        }

        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            cntrol = "Grid";
            forceControl = "Grid";

            getcontrol("this");
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }

        public void getcontrol(string parent)
        {
            switch (cntrol)
            {

                case "Grid":

                    outText.AppendLine("// ");
                    outText.AppendLine("// " + colName);
                    outText.AppendLine("// ");
                    outText.AppendLine("this." + colName + " = new System.Windows.Forms.DataGridViewTextBoxColumn();");

                    outText.AppendLine("this." + colName + ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;");
                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + colName + " .HeaderText = \""  + txt +   "\";");
                    outText.AppendLine(" this." + colName + " .Tag = \"" + dbName +   "\";");
                    outText.AppendLine("this." + colName + ".Name = \"" + colName +   "\";");

                    if (dbName.ToLower() == "id")
                    {
                        outText.AppendLine(" this." + colName + " .Visible = false ;");
                    }

                    break;
            }
        }
    }

    public class clsGridAddRange : BasicFrame, Frame
    {

        public clsGridAddRange()
        {
            forceControl = "Grid";
        }
        public override void GenCreate_Afterlines()
        {
        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine("this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {");
        }

        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendLine("this." + "col" + dbName + ",");
        }
        public override void GenCreate_loop_Lastline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendLine("this." + "col" + dbName + "});");
        }
    }
}
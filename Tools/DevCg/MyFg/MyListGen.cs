using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using myCg;
using System.IO;

namespace MyFg
{

    class MyBothGen : PlugIn.MyClassBase
    {
        public MyBothGen()
            : base()
        {
            Menu.Text = "Both Form";

        }
        public override void Execute()
        {

            string lableEnding = Properties.Settings.Default.LabelEnding;
            bool entWithPanel = Properties.Settings.Default.Entry_WithPanel;
            bool entWtihLable = Properties.Settings.Default.Entry_WithLable;
            string strParent = Properties.Settings.Default.Parent;
            string outputDirectroy = Properties.Settings.Default.OutputDirectory;
            string inputDirectroy = Properties.Settings.Default.InputDirectory;

            if (outputDirectroy.IndexOf('\\') < 1)
            {
                outputDirectroy = Application.StartupPath + "\\" + outputDirectroy;
            }
            if (inputDirectroy.IndexOf('\\') < 1)
            {
                inputDirectroy = Application.StartupPath + "\\" + inputDirectroy;
            }
            //delete all filse in output directory
            foreach (string file in Directory.GetFiles(outputDirectroy))
            {
                File.Delete(file);
            }
            DataSet ds = ICg.ExcelFunction.getData();
            if (ds == null)
                return;
            string tablename = null;
            clsFindReplace fr = new clsFindReplace();

            foreach (DataTable dt in ds.Tables)
            {
                tablename = dt.TableName;
                //entry
                (new MyEntryGen()).entryFormGenerate(tablename, false, dt, inputDirectroy, outputDirectroy, entWithPanel, entWtihLable, strParent);
                //listing
                (new MyListGen()).ListFormGenerate(tablename, false , dt,inputDirectroy ,outputDirectroy );

            }
            try
            {
                Clipboard.SetText(outputDirectroy);
                Process.Start(outputDirectroy);
            }
            catch { }

        }
    }
    class MyListGen : PlugIn.MyClassBase
    {
        public MyListGen()
            : base()
        {
            Menu.Text = "List Form";

        }
        public override void Execute()
        {
            string lableEnding = Properties.Settings.Default.LabelEnding;
            string outputDirectroy = Properties.Settings.Default.OutputDirectory;
            string inputDirectroy = Properties.Settings.Default.InputDirectory;
            if (outputDirectroy.IndexOf('\\') < 1)
            {
                outputDirectroy = Application.StartupPath + "\\" + outputDirectroy;
            }
            if (inputDirectroy.IndexOf('\\') < 1)
            {
                inputDirectroy = Application.StartupPath + "\\" + inputDirectroy;
            }
            //delete all filse in output directory
            foreach (string file in Directory.GetFiles(outputDirectroy))
            {
                File.Delete(file);
            }
            DataSet ds = ICg.ExcelFunction.getData();
            if (ds == null)
                return;
            string tablename = null;
            clsFindReplace fr = new clsFindReplace();
            foreach (DataTable dt in ds.Tables)
            {
                tablename = dt.TableName;
                //entry
              //  entryFormGenerate(tablename, false, dt, inputDirectroy, outputDirectroy, lableEnding);
                //listing
                ListFormGenerate(tablename, false,dt, inputDirectroy, outputDirectroy);

            }
            try
            {
                Clipboard.SetText(outputDirectroy);
                Process.Start(outputDirectroy);
            }
            catch { }

        }


        const string templateListclassname = "frmListSample";
        const string templateEntryclassname = "frmEntrySample";
        public void ListFormGenerate(string tableName, bool blank, DataTable dt, string inputDirectroy, string outputDirectroy)
        {
            clsFindReplace fr = new clsFindReplace();

            string inclassfile = inputDirectroy + "\\" + templateListclassname + ".cs";
            string outclassfile = outputDirectroy + "\\" +  tableName+"_Lst" + ".cs";
            string indesignerfile = inputDirectroy + "\\" + templateListclassname + ".Designer.cs";
            string outdesignerfile = outputDirectroy + "\\" + tableName + "_Lst" + ".Designer.cs";
            string inresourcefile = inputDirectroy + "\\" + templateListclassname + ".resx";
            string outresourcefile = outputDirectroy + "\\" + tableName + "_Lst" + ".resx";
            string genstring = "";
            fr.findReplace(inclassfile, outclassfile, templateListclassname, tableName + "_Lst");
            fr.findReplace(indesignerfile, outdesignerfile, templateListclassname, tableName + "_Lst");
            fr.findReplace(inresourcefile, outresourcefile, templateListclassname, tableName + "_Lst");
            fr.findReplace(outclassfile, outclassfile, templateEntryclassname,   tableName + "_Ent");
            if (!blank)
            {
                clsgridDeclare grideclare = new clsgridDeclare();
                clsGrid grid = new clsGrid();
                clsGridAddRange gridrange = new clsGridAddRange();
                string gridCode = null;

                gridCode = grid.gencreate(dt) + gridrange.gencreate(dt) + "}" + grideclare.gencreate(dt);
                genstring = (new clsListing()).gencreate(dt);
                fr.findReplace(outclassfile, outclassfile, "//<Listing>", "//<Listing>\n" + genstring);
                fr.findReplace(outdesignerfile, outdesignerfile, "}//<Control>", gridCode);
                //for extra text
                fr.findReplace(outdesignerfile, outdesignerfile, "TextExtraValue",grid .table .text  + " List");
            }
        }
        private void entryFormGenerate(string tableName, bool blank, DataTable dt, string inputDirectroy, string outputDirectroy, string lableEnding)
        {
            clsFindReplace fr = new clsFindReplace();
            string infile = inputDirectroy + "\\" + templateEntryclassname;
            string outfile = outputDirectroy + "\\" + tableName + "_Ent";
           
            string genstring = "";
            fr.findReplace(infile + ".cs", outfile + ".cs", templateEntryclassname, tableName + "_Ent");
            fr.findReplace(infile + ".Designer.cs", outfile + ".Designer.cs", templateEntryclassname, tableName + "_Ent");
            fr.findReplace(infile + ".resx", outfile + ".resx", templateEntryclassname, tableName + "_Ent");


            if (!blank)
            {
                clsEntry ent = new clsEntry();
                genstring = ent.gencreate(dt);
                fr.findReplace(outfile + ".cs", outfile + ".cs", "//<Entry>", "//<Entry>\n" + genstring);
                //for form text
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "TextExtraValue", ent.table .text );
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "LableExtraValue", ent.table.text);
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "}//<Control>", myCg.Controlsw.createControls(false, dt, false, "this"));
            }
        }
    }

    public class clsFindReplace
    {
        //public void findReplace(string findfile, string replacefile, string findstring, string replacestring)
        //{
        //    String f1;
        //    Encoding encoding;
        //    using (var reader = new StreamReader(findfile ))
        //    {
        //        f1 = reader.ReadToEnd().ToLower();
        //        encoding = reader.CurrentEncoding;
        //    }

        //    if (f1.Contains(findstring ))
        //    {
        //        f1 = f1.Replace(findstring , replacefile );
        //        File.WriteAllText(replacefile , f1, encoding);
        //    }
        //}
        //public void findReplace(string findfile, string replacefile, string findstring, string replacestring)
        //{
        //    FileInfo FI = new FileInfo(findfile);
        //    try
        //    {
        //        string instr = System.IO.File.ReadAllText (findfile);
        //        instr.Replace(findstring, replacestring);
        //        System.IO.File.WriteAllText(replacefile, instr);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error opening file! Error:" + ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        public void findReplace(string findfile, string replacefile, string findstring, string replacestring)
        {
            FileInfo FI = new FileInfo(findfile);
            try
            {
                StreamReader SR = new StreamReader(FI.OpenRead());
                String s = null;
                String temp = "";
                // int pos = 0;
                s = SR.ReadLine();
                while (s != null)
                {
                    temp = temp + s + Environment.NewLine;
                    s = SR.ReadLine();
                }
                temp = temp.Substring(0, temp.Length);
                SR.Close();
                if (temp.IndexOf(findstring) >= 0)
                {
                    //   pos = temp.IndexOf(findstring);
                    temp = temp.Replace(findstring, replacestring);
                }
                StreamWriter SW = new StreamWriter(replacefile);
                SW.Write(temp);
                SW.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file! Error:" + ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

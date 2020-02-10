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
    //class MyEntryGen : PlugIn.MyClassBase
    //{
    //    public MyEntryGen()
    //        : base()
    //    {
    //        Menu.Text = "Entry Form";

    //    }
    //    public override void Execute()
    //    {
    //        string outputDirectroy = Properties.Settings.Default.OutputDirectory;
    //        string inputDirectroy = Properties.Settings.Default.InputDirectory;
    //        if (outputDirectroy.IndexOf('\\') < 1)
    //        {
    //            outputDirectroy = Application.StartupPath + "\\" + outputDirectroy;
    //        }
    //        if (inputDirectroy.IndexOf('\\') < 1)
    //        {
    //            inputDirectroy = Application.StartupPath + "\\" + inputDirectroy;
    //        }
    //        DataSet ds = ICg.ExcelFunction.getData();
    //        if (ds == null)
    //            return;
    //        string tablename = null;
    //        clsFindReplace fr = new clsFindReplace();
    //        foreach (DataTable dt in ds.Tables)
    //        {
    //            tablename = dt.TableName;
    //            //entry
    //            entryFormGenerate(tablename, false, dt, inputDirectroy, outputDirectroy);
    //            //listing
    //            // listingFormGenerate(tablename, chkBlank.Checked, dt);

    //        }
    //        try
    //        {
    //            Clipboard.SetText(outputDirectroy);
    //            Process.Start(outputDirectroy);
    //        }
    //        catch { }

    //    }


    //    const string templateListclassname = "frmListSample";
    //    const string templateEntryclassname = "frmEntrySample";
    //    public  void entryFormGenerate(string tableName, bool blank, DataTable dt, string inputDirectroy, string outputDirectroy)
    //    {
    //        clsFindReplace fr = new clsFindReplace();
    //        string infile = inputDirectroy + "\\" + templateEntryclassname;
    //        string outfile = outputDirectroy + "\\" + "_Ent" + tableName;

    //        string genstring = "";
    //        fr.findReplace(infile + ".cs", outfile + ".cs", templateEntryclassname, "_Ent" + tableName);
    //        fr.findReplace(infile + ".Designer.cs", outfile + ".Designer.cs", templateEntryclassname, "_Ent" + tableName);
    //        fr.findReplace(infile + ".resx", outfile + ".resx", templateEntryclassname, "_Ent" + tableName);
    //        string controls = "";

    //        if (!blank)
    //        {
    //            genstring = (new clsEntry()).gencreate(dt);
    //            controls = myCg.Controlsw.createControls(true, dt,false);
    //            fr.findReplace(outfile + ".cs", outfile + ".cs", "//<Entry>", "//<Entry>\n" + genstring);
    //            //for form text
    //            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "TextExtraValue", dt.Namespace);
    //            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "LableExtraValue", dt.Namespace);
    //            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "}//<Control>", controls );
              
    //        }
    //    }
    //}

    class MyEntryGen : PlugIn.MyClassBase
    {
        public MyEntryGen()
            : base()
        {
            Menu.Text = "Entry Form";

        }
        public override void Execute()
        { 
            bool entWithPanel = Properties.Settings.Default.Entry_WithPanel;
            bool entWtihLable = Properties.Settings.Default.Entry_WithLable;
            string lableEnding = Properties.Settings.Default.LabelEnding;
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
                entryFormGenerate(tablename, false, dt, inputDirectroy, outputDirectroy, entWithPanel, entWtihLable, strParent);
                //listing
                // listingFormGenerate(tablename, chkBlank.Checked, dt);

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
        public void entryFormGenerate(string tableName, bool blank, DataTable dt, string inputDirectroy, string outputDirectroy, bool withPanel, bool withLable, string parent )
        {
            clsFindReplace fr = new clsFindReplace();
            string infile = inputDirectroy + "\\" + templateEntryclassname;
            string outfile = outputDirectroy + "\\"   + tableName +"_Ent";

            string genstring = "";
            fr.findReplace(infile + ".cs", outfile + ".cs", templateEntryclassname, tableName + "_Ent");
            fr.findReplace(infile + ".Designer.cs", outfile + ".Designer.cs", templateEntryclassname,    tableName + "_Ent");
            fr.findReplace(infile + ".resx", outfile + ".resx", templateEntryclassname, tableName + "_Ent");
            string controls = "";

            if (!blank)
            {

                clsEntry ent = new clsEntry();
                 
                genstring = ent.gencreate(dt);
                controls = myCg.Controlsw.createControls(withLable, dt, withPanel, parent);
                fr.findReplace(outfile + ".cs", outfile + ".cs", "//<Entry>", "//<Entry>\n" + genstring);
                //for form text
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "TextExtraValue", ent.table .text );
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "LableExtraValue", ent.table.text);
                fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "}//<Control>", controls);

            }
        }
    }
}

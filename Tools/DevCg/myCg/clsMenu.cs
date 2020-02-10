using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ICg;
using System.Windows.Forms;
namespace myCg
{
    class myMenuGen : PlugIn.MyClassBase
    {
        public myMenuGen()
            : base()
        {
            Menu.Text = "MenuGen";
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
            string mainmenu = Properties.Settings.Default.MnuGenMainMenuName;
            bool fromList = Properties.Settings.Default.MnuGenFromList;
            foreach (DataTable dt in ds.Tables)
            {
                textout = textout + "\n" + (new clsMenuDeclare(mainmenu, fromList)).gencreate(dt);
                textout = textout + "\n" + (new clsMenuDesigner(mainmenu, fromList)).gencreate(dt);
                textout = textout + "\n" + (new clsMenuDesign(mainmenu, fromList)).gencreate(dt);
                textout = textout + "\n" + (new clsMenuCall(mainmenu, fromList)).gencreate(dt);


            }
            Clipboard.SetText(textout);

        }
    }

    public class clsMenuDesign : BasicFrame, Frame
    {
        string mainMenu = "mnuMasterEntry";
        bool fromList = false;
        public clsMenuDesign(string mainmenu, bool fromlist)
            : base()
        {
            mainMenu = mainmenu;
            fromList = fromlist;
        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine(" // designer keep at top of main form Menu");
            outText.AppendFormat(" this.{0}.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {{", mainMenu);
            outText.AppendLine();
            if (!fromList)
            {
                dbName = table.name;
                txt = table.text;
                outText.AppendFormat(" this.mnu{0},", dbName);
                outText.AppendLine();
              
            }
             

        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop()
        {
            if (fromList)
            {
                outText.AppendFormat(" this.mnu{0},", dbName);
                outText.AppendLine();
            }
        }
        public override void GenCreate_loop_Lastline()
        {
            if (fromList)
            {
                dbName = table.name;
                outText.AppendFormat(" this.mnu{0}", dbName);
            }
            
        }
        public override void GenCreate_Afterlines()
        {
            outText.AppendLine("  });");
        }
    }

    public class clsMenuDeclare : BasicFrame, Frame
    {
        string mainMenu = "mnuMasterEntry";
        bool fromList = false;
        public clsMenuDeclare(string mainmenu, bool fromlist)
            : base()
        {
            mainMenu = mainmenu;
            fromList = fromlist;
        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine(@"//Design keep in declare part");
            if (!fromList )
            {
                dbName = table.name;
                txt = table.text;
              outText.AppendFormat(" private System.Windows.Forms.ToolStripMenuItem mnu{0};      ", dbName, txt);
            outText.AppendLine();
  
            }
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop()
        {
            if (fromList)
            {
                dbName = table.name;
                txt = table.text;
                outText.AppendFormat(" private System.Windows.Forms.ToolStripMenuItem mnu{0};      ", dbName, txt);
                outText.AppendLine();
            }
            
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_Afterlines()
        {

        }
    }

    public class clsMenuCall : BasicFrame, Frame
    {
        string mainMenu = "mnuMasterEntry";
        bool fromList = false;
        public clsMenuCall(string mainmenu, bool fromlist)
            : base()
        {
            mainMenu = mainmenu;
            fromList = fromlist;
        }
        public override void GenCreate_Beforelines()
        {
           
            outText.AppendLine(@"//Keep in main form");
            if (!fromList)
            {
                dbName = table.name;
                txt = table.text;
                outText.AppendFormat(@"
            UEMS.Master_Lst frm{0};
            private void mnu{0}_Click(object sender, EventArgs e)
            {{
                getForm(ref frm{0});
                frm{0}.TableName = ""{0}"";
                frm{0}.Show();
                frm{0}.Activate();
            }}
            ", dbName, txt);
                outText.AppendLine();
            }
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop()
        {
            if (fromList)
            {
                
                outText.AppendFormat(@"
            UEMS.Master_Lst frm{0};
            private void mnu{0}_Click(object sender, EventArgs e)
            {{
                getForm(ref frm{0});
                frm{0}.TableName = ""{0}"";
                frm{0}.Show();
                frm{0}.Activate();
            }}
            ", dbName, txt);
                outText.AppendLine();
            }
           
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_Afterlines()
        {

        }
    }

    public class clsMenuDesigner : BasicFrame, Frame
    {
        string mainMenu = "mnuMasterEntry";
        bool fromList = false;
        public clsMenuDesigner(string mainmenu, bool fromlist)
            : base()
        {
            mainMenu = mainmenu;
            fromList = fromlist;
        }
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine(@"//Keep in main form designer");

            if (!fromList)
            {
                dbName = table.name;
                txt = table.text;
                outText.AppendFormat(@"
             this.mnu{0} = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // mnu{0}
            // 
            this.mnu{0}.Name = ""mnu{0}"";
            this.mnu{0}.Size = new System.Drawing.Size(155, 22);
            this.mnu{0}.Text = ""{1}"";
            this.mnu{0}.Click += new System.EventHandler(this.mnu{0}_Click);
 
           
            ", dbName, txt);
                outText.AppendLine();
            }
           
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop()
        {
            if (fromList)
            {
                dbName = table.name;
                txt = table.text;
                outText.AppendFormat(@"
             this.mnu{0} = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // mnu{0}
            // 
            this.mnu{0}.Name = ""mnu{0}"";
            this.mnu{0}.Size = new System.Drawing.Size(155, 22);
            this.mnu{0}.Text = ""{1}"";
            this.mnu{0}.Click += new System.EventHandler(this.mnu{0}_Click);
 
           
            ", dbName, txt);
                outText.AppendLine();
            }
           
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_Afterlines()
        {

        }
    }


}

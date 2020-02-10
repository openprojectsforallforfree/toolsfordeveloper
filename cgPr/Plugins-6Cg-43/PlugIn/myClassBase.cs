using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlugIn
{
    public class MyClassBase : ImyClass
    {
        public MyClassBase()
        {
            Menu_ = new ToolStripMenuItem("mcbase");
            Menu_.Click += new EventHandler(Menu__Click);
        }
        void Menu__Click(object sender, EventArgs e)
        {
            Execute();
           
        }
        private ToolStripMenuItem Menu_;
        public ToolStripMenuItem Menu
        {
            get
            {
                return Menu_;
            }
            set
            {
                Menu_ = value;
            }
        }

        #region ImyClass Members


        public virtual  bool Execute()
        {
            //ExcelExtension.GetValues egv = new ExcelExtension.GetValues();
            //Cg1.clsDb cd = new Cg1.clsDb();
            //string textout = "";
            //DataSet ds = egv.getValuesFromExcel();
            //if (ds == null)
            //{
            //    return false;
            //}
            //foreach (DataTable dt in ds.Tables)
            //{
            //    textout = textout + "\n" + (new Cg1.clsDb()).gencreate(dt);
            //}
            //Clipboard.SetText(textout);
            //return false;
            MessageBox.Show("hi hello Please override Execute!");
            return false;
        }

        #endregion
    }
}

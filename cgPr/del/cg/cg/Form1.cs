using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cg1;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExcelExtension.GetValues egv = new ExcelExtension.GetValues();
            Cg1.clsDb cd = new Cg1.clsDb();
            string textout = "";
            DataSet ds = egv.getValuesFromExcel();
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
}

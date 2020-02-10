using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class frmOptions : Form
    {
      public   ApplicationSettingsBase settings;
        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           // Properties.Settings.Default.Save();
            settings.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Reload ();
            settings.Reload();
            propertyGrid1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // propertyGrid1.SelectedObject = Properties.Settings.Default;
            propertyGrid1.SelectedObject = settings;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            settings.Reset();
            //Properties.Settings.Default.Reset ();
            propertyGrid1.Refresh();
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mytestDyn
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hihihi");
        }

        //private void Main_Load(object sender, System.EventArgs e)
        //{
        //    MessageBox.Show(ConfigurationSettings.AppSettings["App"]);
        //}

        //private void menuItem1_Click(object sender, System.EventArgs e)
        //{
        //    MessageBox.Show(ConfigurationSettings.AppSettings["App"] + " menuItem1_Click");
        //}
    }
}

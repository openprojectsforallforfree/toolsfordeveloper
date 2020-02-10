using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class frmSelectFile : Form
    {
        public frmSelectFile()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listRecentFile.View = View.Details;
            listRecentFile.GridLines = true;
            listRecentFile.FullRowSelect = true;

            //Add column header
            listRecentFile.Columns.Add("Sno", 40);
            listRecentFile.Columns.Add("Name", 100);
            listRecentFile.Columns.Add("Path", 250);


            string[] arr = new string[4];
            ListViewItem itm;

            List<string> recentFiles = new List<string>();
            recentFiles.Add(@"D:\Other\a.txt");
            recentFiles.Add(@"D:\Otherb\b.txt");
            int i = 1;
            foreach (var item in recentFiles)
            {
                arr[0] = i++.ToString();
                arr[1] = Path.GetFileNameWithoutExtension(item);
                arr[2] = item;

                itm = new ListViewItem(arr);
                listRecentFile.Items.Add(itm);
            }
        }

        private void listRecentFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listRecentFile.SelectedItems.Count >0)
            {
                var v = listRecentFile.SelectedItems[0].SubItems[2];
            }
        }

        private void listRecentFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }
    }
}

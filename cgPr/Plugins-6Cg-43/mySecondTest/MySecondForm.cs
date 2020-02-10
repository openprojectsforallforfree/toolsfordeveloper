using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mySecondTest
{
    public partial class MySecondForm : Form
    {
        public MySecondForm()
        {
            InitializeComponent();
        }
        public string myvar = "Nothing";
            
        private void MySecondForm_Load(object sender, EventArgs e)
        {
            Text = myvar;
        }
    }
}

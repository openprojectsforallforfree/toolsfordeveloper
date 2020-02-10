
using System;
using System.Collections.Generic;
using System.Text;
using PlugIn;
using System.Data;
using System.IO;
using System.Windows.Forms;
using myCg;
using System.Diagnostics;
namespace MyFg
{
    public class MyFg : PlugIn.PlugInBase
    {
        public MyFg()
            : base()
        {
            Menu.Text = "FormG";
            Options = Properties.Settings.Default;
        }
       
    }

    
}

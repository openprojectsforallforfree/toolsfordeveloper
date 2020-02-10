using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PlugIn
{
   public  interface  ImyClass
    {
       ToolStripMenuItem Menu
       {
           get;
           set;
       }
       void Execute();
       
    }
}

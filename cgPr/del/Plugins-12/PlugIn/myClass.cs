using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PlugIn
{
   public  class myClass
    {
       ToolStripMenuItem Menu_;
       public ToolStripMenuItem Menu
       {
           get
           {
               return  Menu_;
           }
           set {Menu_ =value ; }
       }
    }
}

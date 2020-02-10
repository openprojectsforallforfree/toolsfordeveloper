using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myCg
{
    class myDbGen:PlugIn .ImyClass 
    { private ToolStripMenuItem Menu_;
        #region ImyClass Members
        public myDbGen ()
        {
            Menu_ = new ToolStripMenuItem("DbGen");
        }
       
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


        #endregion
    }
}

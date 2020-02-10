using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlugIn;
using System.Windows.Forms;
namespace myCg
{
    public class myCg:IPlugin 
    {
        #region IPlugin Members
        private string _Name;
        public myCg()
        {
            _Name = "dbGen";
            Menu_ = new ToolStripMenuItem("db");
        }
        public int Add(int a, int b)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public IPluginHost Host
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Show()
        {
            throw new NotImplementedException();
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

        public System.Configuration.ApplicationSettingsBase Options
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}

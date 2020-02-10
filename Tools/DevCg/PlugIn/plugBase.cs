using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PlugIn
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
  public   class PlugInBase : IPlugin
    {
        private string m_strName;
        private IPluginHost m_Host;

        public PlugInBase()
        {
            m_strName = "Dhirajiii";
            Menu_ = new ToolStripMenuItem("basemenu");
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
        public Int32 Add(Int32 a, Int32 b)
        { return a + b + 5; }

        public void Show()
        {

        }

        public string Name
        { get { return m_strName; } set { m_strName = value; } }

        public IPluginHost Host
        {
            get { return m_Host; }
            set
            {
                m_Host = value;
                m_Host.Register(this);
            }
        }
        public string myVariable
        { get; set; }

        #region IPlugin Members
        System.Configuration.ApplicationSettingsBase Options_;
        public System.Configuration.ApplicationSettingsBase Options
        {
            get
            {
                return Options_;
            }
            set
            {
                Options_ = value;
            }

        }

        #endregion
    }
  
}

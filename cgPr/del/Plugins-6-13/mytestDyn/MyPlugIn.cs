using System;
using PlugIn;
using mytestDyn;
using System.Windows.Forms;
namespace mytestDyn
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class PlugIn : IPlugin
	{
		private string m_strName;
		private IPluginHost m_Host;
		
		public PlugIn()
		{
			m_strName = "Dhiraj";
              Menu_ = new ToolStripMenuItem("m1");
		}
		
		public Int32 Add(Int32 a, Int32 b)
		{return a + b + 5;}
		
		public void Show()
		{ 
			Main mn = new Main();
			mn.ShowDialog();
		}
		
		public string Name
		{get{return m_strName;}set{m_strName=value;}}

		public IPluginHost Host
		{
			get{return m_Host;}
			set
			{
				m_Host=value;
				m_Host.Register(this);
			}
		}

        #region IPlugin Members

        
        
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
    

        #endregion

        #region IPlugin Members


        public System.Configuration.ApplicationSettingsBase Options
        {
            get
            {
                return null;
            }
        }

        #endregion
    }

    class sm2 : ImyClass
    {
         public sm2()
        {
            Menu_ = new ToolStripMenuItem("sm2");
            Menu_.Click += new EventHandler(Menu__Click);
        }

         void Menu__Click(object sender, EventArgs e)
         {
             MessageBox.Show("hi");
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
    }
    class sm3 : ImyClass
    {
         public sm3()
        {
            Menu_ = new ToolStripMenuItem("sm3");
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
    }
   
}

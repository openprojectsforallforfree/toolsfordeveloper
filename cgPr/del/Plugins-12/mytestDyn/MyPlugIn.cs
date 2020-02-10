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
            Menu = new ToolStripMenuItem("t1");
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


        public ToolStripMenuItem Menu
        {
            get
            {
                return new ToolStripMenuItem("3 dj");
            }
            set { }
        }
    

        #endregion
    }

    class hisclass : myClass
    {
        ToolStripMenuItem Menu_;
        public ToolStripMenuItem Menu
        {
            get
            {
                return Menu_;
            }
            set { Menu_ = value; }
        }
    }
    class herclass : myClass
    {
        ToolStripMenuItem Menu_;
        public ToolStripMenuItem Menu
        {
            get
            {
                return Menu_;
            }
            set { Menu_ = value; }
        }
    }
   
}

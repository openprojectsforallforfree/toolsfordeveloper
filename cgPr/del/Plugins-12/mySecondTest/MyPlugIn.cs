using System;
using PlugIn;
using System.Windows.Forms;
 
namespace mySecondTest
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
			m_strName = "Dhirajiii";
           
		}
		
		public Int32 Add(Int32 a, Int32 b)
		{return a + b + 5;}
		
		public void Show()
		{
            MySecondForm mn = new MySecondForm();
            mn.myvar = myVariable;
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
        public string myVariable
        { get; set; }
        public ToolStripMenuItem Menu { 
            get
        {
                return new ToolStripMenuItem(" 1dj");
        } set{} }
        
	}
    class theirclass : myClass
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

using System;
using PlugIn;

namespace Dynamic2
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
			m_strName = "Dynamic2";
		}
		
		public Int32 Add(Int32 a, Int32 b)
		{return a + b + 5;}
		
		public void Show()
		{ 
			Main1 mn = new Main1();
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
	}
}

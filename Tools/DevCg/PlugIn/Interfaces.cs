using System;
using System.Windows.Forms;
using System.Configuration;

namespace PlugIn
{
	/// <summary>
	/// Generic plugin interface
	/// </summary>
	public interface IPlugin
	{
		Int32 Add(Int32 a, Int32 b);
		string Name{get;set;}
		IPluginHost Host{get;set;}
		void Show();
        ToolStripMenuItem Menu
        { get; set; }
          ApplicationSettingsBase Options
        { get;   }
	}

	/// <summary>
	/// The host
	/// </summary>
	public interface IPluginHost
	{
		bool Register(IPlugin ipi);
	}
}

using System;
using PlugIn;
using System.Reflection;

namespace PlugInTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class PlugInTest : IPluginHost
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <args>Assembly_Name ObjectTypeName</args>
		[STAThread]
		static void Main(string[] args)
		{
			PlugInTest me = new PlugInTest();
			
			me.Start(args);
			
			Console.WriteLine("Press A Key");
			Console.Read();

		}
		public void Start(string[] args)
		{
			Type ObjType = null;
			IPlugin ipi;
			// load the dll
			try
			{
				// load it
				Assembly ass = null;
				ass = Assembly.Load(args[0]);
				if (ass != null)
				{
					ObjType = ass.GetType(args[1]);
				}
			}
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			try
			{
				// OK Lets create the object as we have the Report Type
				if (ObjType != null)
				{
					ipi = (IPlugin)Activator.CreateInstance(ObjType);
					ipi.Host = this;
					Console.WriteLine("ipi.Add(1,2)=" + ipi.Add(1,2));
				}
			}
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public bool Register(IPlugin ipi)
		{
			Console.WriteLine("Registered: " + ipi.Name);
			return true;
		}

		private void Load(object sender, System.EventArgs e)
		{
			
		}
	}
}

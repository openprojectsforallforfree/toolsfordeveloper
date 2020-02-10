using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlugIn;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form, IPluginHost
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }
        private IPlugin[] ipi;
        private void Form1_Load(object sender, System.EventArgs e)
        {
            string path = Application.StartupPath;
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");
            ipi = new IPlugin[pluginFiles.Length];

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                string args = pluginFiles[i].Substring(
                    pluginFiles[i].LastIndexOf("\\") + 1,
                    pluginFiles[i].IndexOf(".dll") -
                    pluginFiles[i].LastIndexOf("\\") - 1);

                Type ObjType = null;
                //IPlugin ipi;
                // load the dll
                try
                {
                    // load it
                    Assembly ass = null;
                    ass = Assembly.Load(args);
                    if (ass != null)
                    {
                        ObjType = ass.GetType(args + ".PlugIn");
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
                        ipi[i] = (IPlugin)Activator.CreateInstance(ObjType);
                        ipi[i].Host = this;
                        ipi[i].myVariable = "I sent this from main";
                        Console.WriteLine("ipi.Add(1,2)=" + ipi[i].Add(1, 2));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //				Start(assemname);
            }
            //MessageBox.Show(ConfigurationSettings.AppSettings["App"]);
        }

        public void Start(string args)
        {
            
        }

        public bool Register(IPlugin ipi)
        {
            MenuItem mn = new MenuItem(ipi.Name, new EventHandler(NewLoad));

            Console.WriteLine("Registered: " + ipi.Name);
            menuItem1.MenuItems.Add(mn);
            return true;
        }

        private void NewLoad(object sender, System.EventArgs e)
        {
            MenuItem mn = (MenuItem)sender;

            for (int i = 0; i < ipi.Length; i++)
            {
                string strType = mn.Text;
                if (ipi[i] != null)
                {
                    if (ipi[i].Name == strType)
                    {
                        ipi[i].Show();
                        break;
                    }
                }
            }
        }

    }
}

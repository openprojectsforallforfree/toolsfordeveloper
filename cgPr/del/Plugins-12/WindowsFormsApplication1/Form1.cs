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
        int i = -1;
        //var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
        //List<myClass> commands = new List<myClass>();
        //foreach (string file in files)
        //{
        //    i++;
        //    Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
        //    if (commandAssembly != null)
        //    {
        //        foreach (Type type in commandAssembly.GetTypes())
        //        {
        //            if (type.BaseType != null && type.BaseType.FullName == typeof(myClass).FullName)
        //            {
        //                commands.Add((myClass)Activator.CreateInstance(type));
        //            }
        //        }
        //    }
        //}
        private void abc()
        {
            var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
            List<myClass> commands = new List<myClass>();
            List<IPlugin> assemblies = new List<IPlugin>();
            List<ToolStripItem> mMenu = new List<ToolStripItem>();
            foreach (string file in files)
            {
                IPlugin plug = null;
                Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
                if (commandAssembly != null)
                {
                    Type ObjType = null;
                    ObjType = commandAssembly.GetType(Path.GetFileNameWithoutExtension(file) + ".PlugIn");
                    if (ObjType != null)
                    {
                        plug = (IPlugin)Activator.CreateInstance(ObjType);
                        plug.Host = this;
                        //plug.Menu = "I sent this from main";
                        Console.WriteLine("ipi.Add(1,2)=" + plug.Add(1, 2));
                        assemblies.Add(plug);
                        //For classes
                        List<ToolStripItem> sMenu = new List<ToolStripItem>();
                        foreach (Type type in commandAssembly.GetTypes())
                        {
                            if (type.BaseType != null && type.BaseType.FullName == typeof(myClass).FullName)
                            {
                                myClass myCls = (myClass)Activator.CreateInstance(type);
                                commands.Add(myCls);
                                sMenu.Add(myCls.Menu);
                                plug.Menu.DropDownItems.Add(myCls.Menu);
                            }
                        }
                       // ToolStripMenuItem ms = new ToolStripMenuItem("ab", null, sMenu.ToArray());
                     //   plug.Menu.DropDownItems.Add(sMenu.ToArray());
                        mMenu.Add(plug .Menu );
                    }
                }
            }
            this.menuStrip1.Items.AddRange(mMenu.ToArray());
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            abc();
        }

        public void Start(string args)
        {

        }

        public bool Register(IPlugin ipi)
        {
            MenuItem mn = new MenuItem(ipi.Name, new EventHandler(NewLoad));
            Console.WriteLine("Registered: " + ipi.Name);
            // menuItem1.MenuItems.Add(mn);
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
        //all class form assemblies
        //private void abc()
        //{
        //    var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
        //    List<myClass> commands = new List<myClass>();
        //    foreach (string file in files)
        //    {
        //        i++;
        //        Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
        //        if (commandAssembly != null)
        //        {
        //            foreach (Type type in commandAssembly.GetTypes())
        //            {
        //                if (type.BaseType != null && type.BaseType.FullName == typeof(myClass).FullName)
        //                {
        //                    commands.Add((myClass)Activator.CreateInstance(type));
        //                }
        //            }
        //        }
        //    }
        //}

        //private void abc()
        //{
        //    var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
        //    List<myClass> commands = new List<myClass>();
        //    List<IPlugin> assemblies = new List<IPlugin>();
        //    foreach (string file in files)
        //    {
        //        IPlugin plug = null;
        //        Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
        //        if (commandAssembly != null)
        //        {
        //            Type ObjType = null;
        //            ObjType = commandAssembly.GetType(Path.GetFileNameWithoutExtension(file) + ".PlugIn");
        //            if (ObjType != null)
        //            {
        //                plug = (IPlugin)Activator.CreateInstance(ObjType);
        //                plug.Host = this;
        //                plug.myVariable = "I sent this from main";
        //                Console.WriteLine("ipi.Add(1,2)=" + plug.Add(1, 2));
        //                assemblies.Add(plug);
        //                //For classes
        //                foreach (Type type in commandAssembly.GetTypes())
        //                {
        //                    if (type.BaseType != null && type.BaseType.FullName == typeof(myClass).FullName)
        //                    {
        //                        commands.Add((myClass)Activator.CreateInstance(type));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //Menu add
        //   List<ToolStripItem> sm = new List<ToolStripItem>();
        //sm.Add(new ToolStripMenuItem ("E"));
        //sm.Add(new ToolStripMenuItem("F"));
        //ToolStripMenuItem ms = new ToolStripMenuItem("ab", null, sm .ToArray ());
        //ToolStripMenuItem ms1 = new ToolStripMenuItem("ab", null, new ToolStripMenuItem("h f"), new ToolStripMenuItem("e"), new ToolStripMenuItem("f"));
        //List<ToolStripItem> mm =new  List<ToolStripItem>();
        //mm.Add ( ms);
        //mm.Add (  ms1);
        //this.menuStrip1.Items.AddRange(mm.ToArray ());

        //plugin
         //string path = Application.StartupPath;
         //   string[] pluginFiles = Directory.GetFiles(path, "*.dll");
         //   ipi = new IPlugin[pluginFiles.Length];

         //   for (int i = 0; i < pluginFiles.Length; i++)
         //   {
         //       string args = Path.GetFileNameWithoutExtension(pluginFiles[i]);
         //       Type ObjType = null;
         //       //IPlugin ipi;
         //       // load the dll
         //       try
         //       {
         //           // load it
         //           Assembly ass = null;
         //           ass = Assembly.Load(args);
         //           if (ass != null)
         //           {
         //               ObjType = ass.GetType(args + ".PlugIn");
         //           }
         //       }
         //       catch (Exception ex)
         //       {
         //           Console.WriteLine(ex.Message);
         //       }
         //       try
         //       {
         //           // OK Lets create the object as we have the Report Type
         //           if (ObjType != null)
         //           {
         //               ipi[i] = (IPlugin)Activator.CreateInstance(ObjType);
         //               ipi[i].Host = this;
         //               ipi[i].myVariable = "I sent this from main";
         //               Console.WriteLine("ipi.Add(1,2)=" + ipi[i].Add(1, 2));
         //           }
         //       }
         //       catch (Exception ex)
         //       {
         //           Console.WriteLine(ex.Message);
         //       }

         //       //				Start(assemname);
         //   }
         //   //MessageBox.Show(ConfigurationSettings.AppSettings["App"]);
    }
}

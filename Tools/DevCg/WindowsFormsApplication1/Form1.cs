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
//loads all dlls with iplugin implementaion 
//only first iplugin is considered
//loads all ImyClass in each dll as sub menu 
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form, IPluginHost
    {
        public Form1()
        {
            InitializeComponent();
        }
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
    
        private void Form1_Load(object sender, System.EventArgs e)
        { 

            LoadPlugins();
            LoadOptions();
        }
        List<ImyClass> commands = new List<ImyClass>();
        List<IPlugin> assemblies = new List<IPlugin>();
        private void LoadOptions()
        {
            var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories).Where (f=> f.Contains ("PlugIn.dll")==false ).ToArray () ;
           
            List<ToolStripItem> sMenuList = new List<ToolStripItem>();

            foreach (string file in files)
            {
                IPlugin plug = null;
                Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
                if (commandAssembly != null)
                {
                      Type ObjType = null;
                      try
                      {
                          var typeo = commandAssembly.GetTypes()
                             .Where(p => typeof(IPlugin).IsAssignableFrom(p) && p.IsInterface == false);
                          ObjType = typeo.ElementAt(0);
                      }
                      catch { }
                    string temp = Path.GetFileNameWithoutExtension(file);
                   // ObjType = commandAssembly.GetType(temp + ".PlugIn");
                    if (ObjType != null)
                    {
                        plug = (IPlugin)Activator.CreateInstance(ObjType);
                        if (plug .Options !=null)
                        {
                            ToolStripMenuItem mnuOptionsItem = new ToolStripMenuItem(plug .Menu .Text );
                        mnuOptionsItem.Click += new EventHandler(mnuOptionsItem_Click);
                        sMenuList.Add(mnuOptionsItem);
                        }
                        
                    }
                }
            }
            ToolStripMenuItem mnuOptions = new ToolStripMenuItem("Options");
            mnuOptions.DropDownItems.AddRange(sMenuList.ToArray());
            this.menuStrip1.Items.Add(mnuOptions);
        }

        void mnuOptionsItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem mn = (ToolStripMenuItem)sender;
             var mens = assemblies.Where (p=> p.Menu .Text    == mn .Text   );
             IPlugin plugOp = (IPlugin)mens.Last();
             frmOptions frmOp = new frmOptions();
             frmOp.settings = plugOp.Options;
             frmOp.Show();
            //for (int i = 0; i < ipi.Length; i++)
            //{
            //    string strType = mn.Text;
            //    if (ipi[i] != null)
            //    {
            //        if (ipi[i].Name == strType)
            //        {
            //            ipi[i].Show();
            //            break;
            //        }
            //    }
            //}
           //((ToolStripMenuItem)sender ).Name 
            //frmOptions f = new frmOptions();
            //f.settings = plug.Options;
            //f.ShowDialog();
        }
        private void LoadPlugins()
        {
            var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories).Where(f => f.Contains("PlugIn.dll") == false).ToArray();
            List<ImyClass> commands = new List<ImyClass>();
            List<ToolStripItem> mMenu = new List<ToolStripItem>();
            foreach (string file in files)
            {
                IPlugin plug = null;
                Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
                if (commandAssembly != null)
                {
                    Type ObjType = null;
                        try{
                    var typeo = commandAssembly.GetTypes()
                       .Where(p => typeof(IPlugin).IsAssignableFrom(p) && p.IsInterface == false);
                    ObjType = typeo.ElementAt (0) ;
                        }catch {}
                  // ObjType = commandAssembly.GetType(Path.GetFileNameWithoutExtension(file) + ".PlugIn");
                    if (ObjType != null)
                    {
                        plug = (IPlugin)Activator.CreateInstance(ObjType);
                        plug.Host = this;
                    
                        assemblies.Add(plug);
                        //For classes
                        List<ToolStripItem> sMenuList = new List<ToolStripItem>();
                        //get all clases implementing Itest
                        //var types = AppDomain.CurrentDomain.GetAssemblies()
                        //    .SelectMany(s => s.GetTypes())
                        //    .Where(p => typeof(ImyClass).IsAssignableFrom(p) && p.IsInterface == false);
                        var types = commandAssembly.GetTypes()
                           .Where(p => typeof(ImyClass).IsAssignableFrom(p) && p.IsInterface == false);
                        foreach (Type type in types)
                        {
                            ImyClass myCls = (ImyClass)Activator.CreateInstance(type);
                            commands.Add(myCls);
                            sMenuList.Add(myCls.Menu);
                        }
                        plug.Menu.DropDownItems.AddRange(sMenuList.ToArray());
                        mMenu.Add(plug.Menu);
                    }
                }
            }
            this.menuStrip1.Items.AddRange(mMenu.ToArray());
        }

        public void Start(string args)
        {

        }

        public bool Register(IPlugin ipi)
        {
            return true;
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






//var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
//            List<ImyClass> commands = new List<ImyClass>();
//            List<IPlugin> assemblies = new List<IPlugin>();
//            List<ToolStripItem> mMenu = new List<ToolStripItem>();
//            foreach (string file in files)
//            {
//                IPlugin plug = null;
//                Assembly commandAssembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));
//                if (commandAssembly != null)
//                {
//                    Type ObjType = null;
//                    ObjType = commandAssembly.GetType(Path.GetFileNameWithoutExtension(file) + ".PlugIn");
//                    if (ObjType != null)
//                    {
//                        plug = (IPlugin)Activator.CreateInstance(ObjType);
//                        plug.Host = this;
//                        //plug.Menu = "I sent this from main";
//                        Console.WriteLine("ipi.Add(1,2)=" + plug.Add(1, 2));
//                        assemblies.Add(plug);
//                        //For classes
//                        List<ToolStripItem> sMenuList = new List<ToolStripItem>();
//                        //get all clases implementing Itest
//                        //var types = AppDomain.CurrentDomain.GetAssemblies()
//                        //    .SelectMany(s => s.GetTypes())
//                        //    .Where(p => typeof(ImyClass).IsAssignableFrom(p) && p.IsInterface == false);
//                        var types = commandAssembly.GetTypes()
//                           .Where(p => typeof(ImyClass).IsAssignableFrom(p) && p.IsInterface == false);
//                        foreach (Type type in types)
//                        {
//                            ImyClass myCls = (ImyClass)Activator.CreateInstance(type);
//                            commands.Add(myCls);
//                            sMenuList.Add(myCls.Menu);
//                        }
//                        plug.Menu.DropDownItems.AddRange(sMenuList.ToArray());
//                        mMenu.Add(plug.Menu);
//                    }
//                }
//            }
//            this.menuStrip1.Items.AddRange(mMenu.ToArray());







    }
}

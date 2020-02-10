//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Xml.Serialization;
//using NMG.Core.Annotations;
//using NMG.Core.Domain;

//namespace NHibernateMappingGenerator
//{
//    public class Project
//    {
//        public Project()
//        {
//            Tables = new List<Tables>();
//            //Connection = new Connection();
//        }
//        public List<Tables> Tables { get; set; }
//       // public Connection Connection { get; set; }
        

//        public void Save(string filepath)
//        {
//            //string filepath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "nmg.rgcg");
//            var streamWriter = new StreamWriter(filepath, false);
//            using (streamWriter)
//            {
//                var xmlSerializer = new XmlSerializer(typeof(Project));
//                xmlSerializer.Serialize(streamWriter, this);
//            }
//        }

//        public static Project Load(string filepath)
//        {
//           // string filepath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "nmg.rgcg");
//            Project project = null;
//            var xmlSerializer = new XmlSerializer(typeof(Project));
//            var fi = new FileInfo(filepath);
//            if (fi.Exists)
//            {
//                using (FileStream fileStream = fi.OpenRead())
//                {
//                    project = (Project)xmlSerializer.Deserialize(fileStream);
//                }
//            }
//            return project;
//        }
//    }

//    //Required for tag
//    public class VisualStudioProject
//    {
//        public string Name { get; set; }
//    }

//    public class VisualStudioFolder
//    {
//        public string FolderName { get; set; }
//        public string FolderPath { get; set; }
//    }
//    //Required for tag
//    public class TemplateFolder
//    {
//        public string Name { get; set; }
//    }
  
//    public class Template
//    {
//        public string TemplateName { get; set; }
//        public string TemplatePath { get; set; }
//        public VisualStudioFolder VisualStudioFolder { get; set; }
//    }

//    public class Tables
//    {
//        public string Name { get; set; }
//        public List<Template> Templates = new List<Template>();
//    }

//}

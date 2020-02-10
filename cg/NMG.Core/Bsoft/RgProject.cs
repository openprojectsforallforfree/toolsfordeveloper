using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMG.Core.Domain;
using System.IO;
using System.Xml.Serialization;


namespace NMG
{
    public class RgProject
    {
        public string SolutionRootFolder { get; set; }
        public string TemplateRootFolder { get; set; }
        public AllTables Alltables { get; set; }
        public List<RgMapping> RgMappings = new List<RgMapping>();

        public void Save(string filename)
        {
            var streamWriter = new StreamWriter(filename, false);
            using (streamWriter)
            {
                var xmlSerializer = new XmlSerializer(this.GetType());
                xmlSerializer.Serialize(streamWriter, this);
            }
        }

        public static RgProject Load(string filename)
        {
            // string filepath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "nmg.rgcg");
            RgProject rgProject = null;
            var xmlSerializer = new XmlSerializer(typeof(RgProject));
            var fi = new FileInfo(filename);
            if (fi.Exists)
            {
                using (FileStream fileStream = fi.OpenRead())
                {
                    rgProject = (RgProject)xmlSerializer.Deserialize(fileStream);
                }
            }
            return rgProject;
        }
    }

    public class RgMapping
    {
        public List <string > TemplateRelativePaths { get; set; } 
        public string TableName { get; set; }
        public RgMapping()
        {
            TemplateRelativePaths = new List<string>();
            TableName = string.Empty;
        }
    }


    public class AllTables
    {
        public List<Table> Tables { get; set; }
        public String DataBaseName { get; set; }

        public void Save(string filename)
        {
            var streamWriter = new StreamWriter(filename, false);
            using (streamWriter)
            {
                var xmlSerializer = new XmlSerializer(this.GetType());
                xmlSerializer.Serialize(streamWriter, this);
            }
        }

        public static AllTables Load(string filename)
        {
            // string filepath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "nmg.rgcg");
            AllTables alltables = null;
            var xmlSerializer = new XmlSerializer(typeof(AllTables));
            var fi = new FileInfo(filename);
            if (fi.Exists)
            {
                using (FileStream fileStream = fi.OpenRead())
                {
                    alltables = (AllTables)xmlSerializer.Deserialize(fileStream);
                }
            }
            return alltables;
        }
    }

}

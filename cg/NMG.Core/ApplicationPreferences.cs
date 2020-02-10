using System.Collections.Generic;
using NMG.Core.Domain;

namespace NMG.Core
{
    public class ApplicationPreferences
    {
        public ApplicationPreferences()
        {
            FieldNamingConvention = FieldNamingConvention.SameAsDatabase;
            FieldGenerationConvention = FieldGenerationConvention.Field;
          
        }

        public string TableName { get; set; }

        public string FolderPath { get; set; }

        public string DomainFolderPath { get; set; }

        public string NameSpace { get; set; }

        public string NameSpaceMap { get; set; }

        public string AssemblyName { get; set; }

        public ServerType ServerType { get; set; }

        public string ConnectionString { get; set; }

        public string Sequence { get; set; }

        

      

        

         
        
 

        public FieldNamingConvention FieldNamingConvention { get; set; }

        public FieldGenerationConvention FieldGenerationConvention { get; set; }

        public string EntityName { get; set; }

        

      

       
    }
}
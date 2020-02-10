using NMG.Core;
using NMG.Core.Domain;
 

namespace NHibernateMappingGenerator
{
    public class ApplicationController
    {
        private readonly ApplicationPreferences applicationPreferences;
        

        public ApplicationController(ApplicationPreferences applicationPreferences, Table table)
        {
            this.applicationPreferences = applicationPreferences;
           
        }

        public string GeneratedDomainCode { get; set; }
        public string GeneratedMapCode { get; set; }

       
    }
}
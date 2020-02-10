using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Organization:AuditInfo
    {
        public Organization()
        {
            this.Surveys = new List<Survey>();
        }

        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}

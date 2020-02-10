using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Survey:AuditInfo
    {
        public Survey()
        {
            this.GroupSurveys = new List<GroupSurvey>();
            this.SurveySections = new List<SurveySection>();
        }

        public int SurveyId { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int OrganizationId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<GroupSurvey> GroupSurveys { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<SurveySection> SurveySections { get; set; }
    }
}

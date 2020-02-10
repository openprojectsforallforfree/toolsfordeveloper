using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class GroupSurvey:AuditInfo
    {
        public GroupSurvey()
        {
            this.UserTablets = new List<UserTablet>();
        }

        public int GroupSurveyId { get; set; }
        public int GroupId { get; set; }
        public int SurveyId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Group Group { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<UserTablet> UserTablets { get; set; }
    }
}

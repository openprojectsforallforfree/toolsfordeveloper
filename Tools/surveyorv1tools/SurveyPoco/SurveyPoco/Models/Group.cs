using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Group:AuditInfo
    {
        public Group()
        {
            this.GroupSurveys = new List<GroupSurvey>();
            this.UserGroups = new List<UserGroup>();
        }

        public int GroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<GroupSurvey> GroupSurveys { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

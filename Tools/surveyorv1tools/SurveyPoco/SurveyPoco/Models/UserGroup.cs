using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class UserGroup:AuditInfo
    {
        public int UserGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsSuperVisor { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}

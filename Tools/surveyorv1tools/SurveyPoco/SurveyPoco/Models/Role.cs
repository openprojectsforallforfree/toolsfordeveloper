using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Role:AuditInfo
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

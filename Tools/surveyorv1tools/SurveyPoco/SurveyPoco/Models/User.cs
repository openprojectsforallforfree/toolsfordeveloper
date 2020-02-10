using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class User:AuditInfo
    {
        public User()
        {
            this.SurveyeeSurveyComments = new List<SurveyeeSurveyComment>();
            this.UserGroups = new List<UserGroup>();
            this.UserTablets = new List<UserTablet>();
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int PwdChangeDays { get; set; }
        public int PwdChangeWarningDays { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SurveyeeSurveyComment> SurveyeeSurveyComments { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserTablet> UserTablets { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class UserTablet:AuditInfo
    {
        public int UserTabletId { get; set; }
        public int UserId { get; set; }
        public int TabletId { get; set; }
        public int GroupSurveyId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual GroupSurvey GroupSurvey { get; set; }
        public virtual Tablet Tablet { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Tablet:AuditInfo
    {
        public Tablet()
        {
            this.UserTablets = new List<UserTablet>();
        }

        public int TabletId { get; set; }
        public string Code { get; set; }
        public string Mac { get; set; }
        public string OperatingSystem { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserTablet> UserTablets { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class PredefinedDropdownValue:AuditInfo
    {
        public int PredefinedDropdownValueId { get; set; }
        public int PredefinedDropdownId { get; set; }
        public int ParentValueId { get; set; }
        public string Code { get; set; }
        public string PDDValue { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual PredefinedDropdown PredefinedDropdown { get; set; }
    }
}

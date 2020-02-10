using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class PredefinedDropdown:AuditInfo
    {
        public PredefinedDropdown()
        {
            this.PredefinedDropdownValues = new List<PredefinedDropdownValue>();
            this.Questions = new List<Question>();
        }

        public int PredefinedDropdownId { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public int ParentTableId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<PredefinedDropdownValue> PredefinedDropdownValues { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}

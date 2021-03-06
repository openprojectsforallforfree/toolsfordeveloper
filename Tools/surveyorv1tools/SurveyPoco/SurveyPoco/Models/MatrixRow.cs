using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class MatrixRow:AuditInfo
    {
        public int RowId { get; set; }
        public string RowLabel { get; set; }
        public int QuestionId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }
    }
}

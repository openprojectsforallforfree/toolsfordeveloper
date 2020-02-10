using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class MatrixAnswerComment:AuditInfo
    {
        public string MatrixAnswerCommentId { get; set; }
        public Nullable<int> MatrixQuestionId { get; set; }
        public string Comment { get; set; }
        public string MatrixAnswerSetGuid { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual MatrixAnswerSet MatrixAnswerSet { get; set; }
    }
}

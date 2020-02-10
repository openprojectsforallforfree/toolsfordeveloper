using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class MatrixAnswerSet:AuditInfo
    {
        public MatrixAnswerSet()
        {
            this.MatrixAnswers = new List<MatrixAnswer>();
            this.MatrixAnswerComments = new List<MatrixAnswerComment>();
        }

        public string MatrixAnswerSetGuid { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AnswerId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual ICollection<MatrixAnswer> MatrixAnswers { get; set; }
        public virtual ICollection<MatrixAnswerComment> MatrixAnswerComments { get; set; }
    }
}

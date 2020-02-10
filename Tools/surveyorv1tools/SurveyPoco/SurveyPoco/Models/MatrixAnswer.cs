using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class MatrixAnswer:AuditInfo
    {
        public string MatrixAnswerId { get; set; }
        public Nullable<int> MatrixQuestionId { get; set; }
        public Nullable<int> QuestionOptionId { get; set; }
        public string AnswerValue { get; set; }
        public string Comment { get; set; }
        public string AnswerId { get; set; }
        public string AnswerSetId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string MatrixAnswerSet_MatrixAnswerSetGuid { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual MatrixAnswerSet MatrixAnswerSet { get; set; }
        public virtual MatrixQuestion MatrixQuestion { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }
}

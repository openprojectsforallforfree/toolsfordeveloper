using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Answer:AuditInfo
    {
        public Answer()
        {
            this.MatrixAnswers = new List<MatrixAnswer>();
            this.MatrixAnswerSets = new List<MatrixAnswerSet>();
        }

        public string AnswerId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> QuestionOptionId { get; set; }
        public string AnswerValue { get; set; }
        public string Comment { get; set; }
        public string SurveyeeGuid { get; set; }
        public Nullable<int> MatrixColId { get; set; }
        public Nullable<int> MatrixRowId { get; set; }
        public Nullable<int> MatrixQuestionId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
        public virtual Surveyee Surveyee { get; set; }
        public virtual ICollection<MatrixAnswer> MatrixAnswers { get; set; }
        public virtual ICollection<MatrixAnswerSet> MatrixAnswerSets { get; set; }
    }
}

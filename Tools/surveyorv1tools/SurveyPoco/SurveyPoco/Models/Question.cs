using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Question:AuditInfo
    {
        public Question()
        {
            this.Answers = new List<Answer>();
            this.MatrixCols = new List<MatrixCol>();
            this.MatrixQuestions = new List<MatrixQuestion>();
            this.MatrixRows = new List<MatrixRow>();
            this.QuestionJumpLogics = new List<QuestionJumpLogic>();
            this.QuestionOptions = new List<QuestionOption>();
        }

        public int QuestionId { get; set; }
        public int PrecedingQuestionId { get; set; }
        public string Code { get; set; }
        public string QuestionGroup { get; set; }
        public string QuestionText { get; set; }
        public bool Required { get; set; }
        public bool OnlyNumericValue { get; set; }
        public bool IncludeComment { get; set; }
        public string Comment { get; set; }
        public int QuestionOrder { get; set; }
        public int DependentQuestionId { get; set; }
        public int DependentQuestionOptionId { get; set; }
        public bool AllowMultipleChoice { get; set; }
        public bool HasPredefinedDropdown { get; set; }
        public int PredefinedDropdownId { get; set; }
        public bool IsUpdated { get; set; }
        public int NoOfRows { get; set; }
        public int SectionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int RuleId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOther { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<MatrixCol> MatrixCols { get; set; }
        public virtual ICollection<MatrixQuestion> MatrixQuestions { get; set; }
        public virtual ICollection<MatrixRow> MatrixRows { get; set; }
        public virtual PredefinedDropdown PredefinedDropdown { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual SurveySection SurveySection { get; set; }
        public virtual ICollection<QuestionJumpLogic> QuestionJumpLogics { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}

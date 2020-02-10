using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class QuestionOption:AuditInfo
    {
        public QuestionOption()
        {
            this.Answers = new List<Answer>();
            this.MatrixAnswers = new List<MatrixAnswer>();
        }

        public int QuestionOptionId { get; set; }
        public string OptionChoiceLabel { get; set; }
        public bool IsDropdown { get; set; }
        public int QuestionId { get; set; }
        public string DataType { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> MatrixQuestion_MatrixQuestionId { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<MatrixAnswer> MatrixAnswers { get; set; }
        public virtual MatrixQuestion MatrixQuestion { get; set; }
        public virtual Question Question { get; set; }
    }
}

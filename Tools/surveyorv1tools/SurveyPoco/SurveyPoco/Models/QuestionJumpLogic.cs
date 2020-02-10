using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class QuestionJumpLogic:AuditInfo
    {
        public int QuestionJumpLogicId { get; set; }
        public int JumpQuestionId { get; set; }
        public string JumpQuestionOptionId { get; set; }
        public int QuestionId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> MatrixQuestion_MatrixQuestionId { get; set; }
        public virtual MatrixQuestion MatrixQuestion { get; set; }
        public virtual Question Question { get; set; }
    }
}

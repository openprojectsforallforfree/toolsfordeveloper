using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class AnswerComment:AuditInfo
    {
        public string QuestionCommentId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public string Comment { get; set; }
        public string SurveyeeGuid { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Surveyee Surveyee { get; set; }
    }
}

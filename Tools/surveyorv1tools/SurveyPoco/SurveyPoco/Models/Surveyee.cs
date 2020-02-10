using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Surveyee:AuditInfo
    {
        public Surveyee()
        {
            this.Answers = new List<Answer>();
            this.AnswerComments = new List<AnswerComment>();
            this.SurveyeeSurveyComments = new List<SurveyeeSurveyComment>();
        }

        public string SurveyeeGuId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        public virtual ICollection<SurveyeeSurveyComment> SurveyeeSurveyComments { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class SurveyeeSurveyComment:AuditInfo
    {
        public string SurveyCommentsId { get; set; }
        public string SurveyeeGuid { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Surveyee Surveyee { get; set; }
        public virtual User User { get; set; }
    }
}

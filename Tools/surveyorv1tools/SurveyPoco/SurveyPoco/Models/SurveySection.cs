using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class SurveySection:AuditInfo
    {
        public SurveySection()
        {
            this.Questions = new List<Question>();
        }

        public int SectionId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int SurveyId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Survey Survey { get; set; }
    }
}

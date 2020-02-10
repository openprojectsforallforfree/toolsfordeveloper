using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class MenuItem:AuditInfo
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string LinkText { get; set; }
        public int ModuleId { get; set; }
        public Nullable<int> ParentId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class Rule:AuditInfo
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

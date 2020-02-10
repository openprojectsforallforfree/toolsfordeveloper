using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class OptionChoice:AuditInfo
    {
        public int OptionChoiceId { get; set; }
        public string OptionChoiceLabel { get; set; }
        public bool IsDropdown { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class OptionError:AuditInfo
    {
        public int OptionErrorId { get; set; }
        public int QuestionOptionId { get; set; }
        public string ErrorText { get; set; }
    }
}

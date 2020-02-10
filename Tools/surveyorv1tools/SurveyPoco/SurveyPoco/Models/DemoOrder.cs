using System;
using System.Collections.Generic;

namespace SurveyCore.Models
{
    public partial class DemoOrder:AuditInfo
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public int HeaderId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NPGeekEF.Models
{
    public partial class SurveyResult
    {
        public int SurveyId { get; set; }
        public string ParkCode { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }

        public virtual Park ParkCodeNavigation { get; set; }
    }
}

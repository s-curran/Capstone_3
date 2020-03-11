using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.Models
{
    public class SurveyResultsViewModel
    {
        public string ParkCode { get; set; }
        public int NumVotes { get; set; }
        public string ParkName { get; set; }
        public string ParkLocation { get; set; }
        public string ParkClimate { get; set; }
    }
}

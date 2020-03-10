using System;
using System.Collections.Generic;

namespace NPGeekEF.Models
{
    public partial class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public virtual Park ParkCodeNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPGeekEF.Models
{
    public partial class Park
    {
        public Park()
        {
            SurveyResult = new List<SurveyResult>();
            Weather = new List<Weather>();
        }

        [Required(ErrorMessage = "Enter Park Code")]
        [StringLength(4)]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "Enter Park Name")]
        public string ParkName { get; set; }

        [Required(ErrorMessage = "*")]
        public string State { get; set; }
        [Required(ErrorMessage = "*")]
        public int Acreage { get; set; }
        [Required(ErrorMessage = "*")]
        public int ElevationInFeet { get; set; }
        [Required(ErrorMessage = "*")]
        public float MilesOfTrail { get; set; }
        [Required(ErrorMessage = "*")]
        public int NumberOfCampsites { get; set; }
        [Required(ErrorMessage = "*")]
        public string Climate { get; set; }
        [Required(ErrorMessage = "*")]
        public int YearFounded { get; set; }
        [Required(ErrorMessage = "*")]
        public int AnnualVisitorCount { get; set; }
        
        public string InspirationalQuote { get; set; }
        public string InspirationalQuoteSource { get; set; }
        [Required(ErrorMessage = "*")]
        public string ParkDescription { get; set; }
        [Required(ErrorMessage = "*")]
        public int EntryFee { get; set; }
        [Required(ErrorMessage = "*")]
        public int NumberOfAnimalSpecies { get; set; }

        public virtual ICollection<SurveyResult> SurveyResult { get; set; }
        public virtual ICollection<Weather> Weather { get; set; }
    }
}

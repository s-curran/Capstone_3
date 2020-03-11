using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.Models
{
    public class SurveyViewModel
    {

        [Required]
        public string ParkCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ActivityLevel { get; set; }

        public SurveyResult Survey { get; set; }

        public void BuildSurveyObject()
        {
            Survey = new SurveyResult();
            Survey.ParkCode = ParkCode;
            Survey.EmailAddress = Email;
            Survey.State = State;
            Survey.ActivityLevel = ActivityLevel;
        }
    }
}

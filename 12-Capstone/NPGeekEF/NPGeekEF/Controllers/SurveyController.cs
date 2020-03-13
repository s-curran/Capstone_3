using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPGeekEF.DAL;
using NPGeekEF.Models;
using TE.AuthLib;

namespace NPGeekEF.Controllers
{
    public class SurveyController : NPGeekBaseController
    {
        private IParksDAO ParksDAO;
        private ISurveyDAO SurveyDAO;

        public SurveyController(IParksDAO parksDAO, ISurveyDAO surveyDAO, IAuthProvider authProvider) : base(authProvider)
        {
            this.ParksDAO = parksDAO;
            this.SurveyDAO = surveyDAO;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["parks"] = parkList;
            ViewData["states"] = SelectListHelper.stateList;
            ViewData["activities"] = SelectListHelper.activityList;
            return View();
        }

        [HttpPost]
        public IActionResult Index(SurveyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // Build survey object
            vm.BuildSurveyObject();

            // Add Survey to DB
            bool success = SurveyDAO.AddSurveyResult(vm.Survey);

            if (success)
            {
                TempData["message"] = "Your survey results were recorded!";
            }

            return RedirectToAction("SurveyResults");
        }

        public IActionResult SurveyResults()
        {
            List<SurveyResultsViewModel> vm = BuildSurveyResults();
            return View(vm);
        }

        private List<SurveyResultsViewModel> BuildSurveyResults()
        {
            Dictionary<string, int> results = SurveyDAO.GetSurveyResults();
            List<SurveyResultsViewModel> vmList = new List<SurveyResultsViewModel>();

            foreach (KeyValuePair<string, int> kvp in results)
            {
                SurveyResultsViewModel vm = new SurveyResultsViewModel();
                vm.ParkCode = kvp.Key;
                vm.NumVotes = kvp.Value;

                Park park = ParksDAO.GetParkByCode(vm.ParkCode);
                vm.ParkName = park.ParkName;
                vm.ParkLocation = park.State;
                vm.ParkClimate = park.Climate;
                vmList.Add(vm);
            }

            return vmList;
        }

        private List<SelectListItem> parkList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();


                foreach (Park park in ParksDAO.GetAllParks())
                {
                    items.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
                }
                return items;
            }
        }
    }
}

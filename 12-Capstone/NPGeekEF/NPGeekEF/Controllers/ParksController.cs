using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPGeekEF.DAL;
using NPGeekEF.Models;
using TE.AuthLib;

namespace NPGeekEF.Controllers
{
    public class ParksController : NPGeekBaseController
    {
        private IParksDAO ParksDAO;
        private IWeatherDAO WeatherDAO;
        public ParksController(IParksDAO parksDAO, IWeatherDAO weatherDAO, IAuthProvider authProvider) : base(authProvider)
        {
            this.ParksDAO = parksDAO;
            this.WeatherDAO = weatherDAO;
        }
        public IActionResult Index()
        {
            return View(ParksDAO.GetAllParks());
        }
        public IActionResult Detail(string code, string tempPref)
        {
            Park park = ParksDAO.GetParkByCode(code);
            park.Weather = WeatherDAO.GetWeatherByParkCode(park.ParkCode);

            ParkDetailViewModel vm = new ParkDetailViewModel();
            vm.Park = park;
            vm.SetAlert(park);

            string tempChoice;

            if (IsLoggedIn)
            {
                if (tempPref != null)
                {
                    authProvider.UpdateTempPref(tempPref);
                }
                User user = authProvider.GetCurrentUser();

                tempChoice = user.TempPref;
            }
            else
            {
                string temp = HttpContext.Session.GetString("TempChoice");

                HttpContext.Session.SetString("TempChoice", tempPref ?? temp ?? "F");

                tempChoice = HttpContext.Session.GetString("TempChoice");
            }


            if (tempChoice == "C")
            {
                vm.IsCelsius = true;
            }
            else
            {
                vm.IsCelsius = false;
            }

            return View(vm);
        }

        public IActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPark()
        {
            ViewData["states"] = SelectListHelper.stateList;
            return View();
        }

        [HttpPost]
        public IActionResult AddPark(Park park)
        {
            if (!ModelState.IsValid)
            {
                return View(park);
            }

            bool success = ParksDAO.AddNewPark(park);
            TempData["message"] = success;

            return RedirectToAction();
        }
    }
}
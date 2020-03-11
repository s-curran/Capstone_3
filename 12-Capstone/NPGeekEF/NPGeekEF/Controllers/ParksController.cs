using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPGeekEF.DAL;
using NPGeekEF.Models;

namespace NPGeekEF.Controllers
{
    public class ParksController : Controller
    {
        private IParksDAO ParksDAO;
        private IWeatherDAO WeatherDAO;
        public ParksController(IParksDAO parksDAO, IWeatherDAO weatherDAO)
        {
            this.ParksDAO = parksDAO;
            this.WeatherDAO = weatherDAO;
        }
        public IActionResult Index()
        {
            return View(ParksDAO.GetAllParks());
        }
        public IActionResult Detail(string code)
        {
            Park park = ParksDAO.GetParkByCode(code);
            park.Weather = WeatherDAO.GetWeatherByParkCode(park.ParkCode);
            return View();
        }

        public IActionResult Survey()
        {
            return View();
        }
    }
}
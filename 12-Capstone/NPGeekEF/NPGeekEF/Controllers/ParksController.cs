using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPGeekEF.DAL;

namespace NPGeekEF.Controllers
{
    public class ParksController : Controller
    {
        private IParksDAO ParksDAO;
        public ParksController(IParksDAO parksDAO)
        {
            this.ParksDAO = parksDAO;
        }
        public IActionResult Index()
        {
            return View(ParksDAO.GetAllParks());
        }
        public IActionResult Detail(string code)
        {

            return View();
        }

        public IActionResult Survey()
        {
            return View();
        }
    }
}
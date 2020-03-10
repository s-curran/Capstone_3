using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NPGeekEF.Controllers
{
    public class ParksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Survey()
        {
            return View();
        }
    }
}
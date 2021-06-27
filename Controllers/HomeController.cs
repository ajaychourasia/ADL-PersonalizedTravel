using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ADL.PersonalizedTravel.Models;
using ADL.PersonalizedTravel.Services;
using ADL.PersonalizedTravel.Controllers.Extensions;
using ADL.PersonalizedTravel.Repositories;
using Microsoft.AspNetCore.Identity;
using ADL.PersonalizedTravel.Utilities;

namespace ADL.PersonalizedTravel.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPersonalizerService _service;
        private readonly ITourRepository _tourRepository;
      
        public HomeController(IPersonalizerService service, ITourRepository tourRepository)
        {
            _service = service;
            _tourRepository = tourRepository;
         }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Explore more about Enthociast Tour & Travel.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact us for more fun with travel.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

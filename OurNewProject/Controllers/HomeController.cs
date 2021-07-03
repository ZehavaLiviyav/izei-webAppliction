using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OurNewProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Controllers
{

    // 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
           // comment
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rishon(){ return View();}
        public IActionResult BeerS() { return View(); }
        public IActionResult Eilat() { return View(); }
        public IActionResult Jerusalem() { return View(); }
        public IActionResult RG() { return View(); }
        public IActionResult Shoam() { return View(); }
        public IActionResult TA() { return View(); }
        public IActionResult Mevasseret() { return View(); }
        public IActionResult Hyadata() { return View(); }

     
        

        //  [Authorize]
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

using BSFP.Areas.Identity.Data;
using BSFP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Controllers
{
    public class InformatieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<CustomUser> _signInManager;
        public InformatieController(ILogger<HomeController> logger, SignInManager<CustomUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult PaardenpuntVlaanderen()
        {
            return View();
        }
        public IActionResult FriesPaard()
        {
            return View();
        }
        public IActionResult JongBSFP()
        {
            return View();
        }
        public IActionResult LidWorden()
        {
            return View();
        }
        public IActionResult IndentificatiePaard()
        {
            return View();
        }
        public IActionResult Mennen()
        {
            return View();
        }
        public IActionResult Infodagen()
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

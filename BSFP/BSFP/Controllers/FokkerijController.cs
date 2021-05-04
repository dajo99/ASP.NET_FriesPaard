using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Controllers
{
    public class FokkerijController : Controller
    {
        public IActionResult Fokdoel()
        {
            return View();
        }

        public IActionResult Fokprogramma()
        {
            return View();
        }

        public IActionResult ActieveHengsten()
        {
            return View();
        }

        public IActionResult DNATest()
        {
            return View();
        }

        public IActionResult Hengstenkeuze()
        {
            return View();
        }

        public IActionResult InteeltVerwantschap()
        {
            return View();
        }
        

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Controllers
{
    public class ReglementenController : Controller
    {
        public IActionResult Geschillenregeling()
        {
            return View();
        }

        public IActionResult InterneReglementen()
        {
            return View();
        }

        public IActionResult ProtocolAankoopPaard()
        {
            return View();
        }

        public IActionResult Registratiereglement()
        {
            return View();
        }

        public IActionResult Statuten()
        {
            return View();
        }

        public IActionResult Tarieven()
        {
            return View();
        }

        public IActionResult VaststellenRegistrerenValideren()
        {
            return View();
        }

    }
}

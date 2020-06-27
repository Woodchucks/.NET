using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Labo7_vol2.Models;
using Microsoft.AspNetCore.Http;

namespace Labo7_vol2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public ActionResult SesjaCiasteczka() 
        { 
            string message = HttpContext.Session.GetString("Message"); 
            if (message != null) ViewBag.Message = message; 
            else ViewBag.Message = "Na razie wartośc zmiennej sesyjnej Message nie została ustawiona."; 
            if (Request.Cookies["ciasteczkoKrotkie"] != null) ViewBag.CiasteczkoKrotkie = Request.Cookies["ciasteczkoKrotkie"]; 
            else ViewBag.CiasteczkoKrotkie = "brak"; 
            if (Request.Cookies["ciasteczkoDlugie"] != null) ViewBag.CiasteczkoDlugie = Request.Cookies["ciasteczkoDlugie"]; 
            else ViewBag.CiasteczkoDlugie = "brak"; return View(); 
        }
        public ActionResult Cookie1()
        {
            CookieOptions option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddSeconds(15); 
            Response.Cookies.Append("ciasteczkoKrotkie", "Tu jest krótkie ciasteczko", option); 
            option = new CookieOptions(); 
            option.Expires = DateTime.Now.AddMinutes(10); 
            Response.Cookies.Append("ciasteczkoDlugie", "Tu jest długie ciasteczko", option);

            return RedirectToAction("SesjaCiasteczka");
        }

        public ActionResult Session1() 
        {
            HttpContext.Session.SetString("Message", "Message został ustawiony poprzez zmienną sesyjną");
            return RedirectToAction("SesjaCiasteczka"); 
        }
    }
}

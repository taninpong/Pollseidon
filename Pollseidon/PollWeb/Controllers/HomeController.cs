using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollWeb.Models;

namespace PollWeb.Controllers
{
    public class HomeController : Controller
    {
        public static string username;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string UsernameInput)
        {
            username = UsernameInput;
            return RedirectToAction(nameof(Main));
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

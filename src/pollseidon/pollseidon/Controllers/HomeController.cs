using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pollseidon.Models;

namespace pollseidon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Username)
        {
            return RedirectToAction(nameof(Topics));
        }

        public IActionResult Topics()
        {
            return View();
        }

        public IActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTopic(object topic)
        {
            return RedirectToAction(nameof(Topics));
        }

        public IActionResult Vote(string TopicId)
        {
            return View();
        }

        public IActionResult SendVote()
        {
            return RedirectToAction(nameof(Vote));
        }

        public IActionResult AddChoice(string topicId)
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddChoice(string topicId, object choice)
        {
            return RedirectToAction(nameof(Vote));
        }

        public IActionResult ViewVote(string topicId, string choiceId)
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

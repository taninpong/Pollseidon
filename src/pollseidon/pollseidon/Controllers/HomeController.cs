using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pollseidon.facade.Facade;
using pollseidon.Models;

namespace pollseidon.Controllers
{
    public class HomeController : Controller
    {
        public Facade facade;
        public static string username;
        public HomeController(Facade _facade)
        {
            facade = _facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Username)
        {
            username = Username;
            return RedirectToAction(nameof(Topics));
        }

        public IActionResult Topics()
        {
            var model = facade.GetPoll();
            return View();
        }

        public IActionResult MyTopics()
        {
            var model = facade.GetMyPoll(username);
            return View();
        }

        public IActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTopic(Topic topic)
        {
            topic.id = Guid.NewGuid().ToString();
            topic.CreateDate = DateTime.Now;
            topic.CreateBy = username;
            topic.TopicName = topic.TopicName;
            if (!string.IsNullOrEmpty(topic.TopicName))
                topic.ChoiceList = new List<Choice> { new Choice { Id = Guid.NewGuid().ToString(), Name = topic.TopicName } };
            facade.CreateTopic(topic, username);
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

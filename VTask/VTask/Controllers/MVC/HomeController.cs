using Microsoft.AspNetCore.Mvc;

namespace VTask.Controllers.MVC
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}

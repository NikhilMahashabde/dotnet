using Microsoft.AspNetCore.Mvc;
using SessionASPCore6.Models;
using System.Diagnostics;

namespace SessionASPCore6.Controllers
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
            HttpContext.Session.SetString("id", "nikhil");
            return View();
        }

        public IActionResult About()
        {
            var id = HttpContext.Session.GetString("id");

            if (id != null)

            {
                ViewBag.Data = id.ToString();
            }

            return View();
        }

        public IActionResult Details()
        {
            var id = HttpContext.Session.GetString("id");

            if (id != null)

            {
                ViewBag.Data = id.ToString();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {

            if (HttpContext.Session.GetString("id") != null)
            {
                HttpContext.Session.Remove("id");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
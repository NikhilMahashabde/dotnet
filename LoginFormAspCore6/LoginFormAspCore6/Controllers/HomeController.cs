using LoginFormAspCore6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginFormAspCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext myDbContext;

        public HomeController(ILogger<HomeController> logger, MyDbContext myDbContext)
        {
            _logger = logger;
            this.myDbContext = myDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {

            var myUser = myDbContext.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

            if (myUser != null)
            {
                HttpContext.Session.SetString("id", myUser.Email);
                return RedirectToAction("Dashboard");

            }
            else
            {
                ViewBag.Message = "Login Failed..";
            }

            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("id") != null)
            {

                ViewBag.MySession = HttpContext.Session.GetString("id").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("id") != null)
            {

                HttpContext.Session.Remove("id");
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }

            try
            {

                await myDbContext.Users.AddAsync(user);
                await myDbContext.SaveChangesAsync();
                TempData["success"] = "Registered Sucesfully";
            }
            catch (Exception ex)
            {
                return RedirectToAction("Register");
            }

            return RedirectToAction("Login");
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
using CodeFirstAspCore6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstAspCore6.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly StudentDbContext studentDb;

        public HomeController(StudentDbContext studentDb)
        {
            this.studentDb = studentDb;
        }

        public async Task<IActionResult> Index()
        {

            var stdData = await studentDb.Students.ToListAsync();
            return View(stdData);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDb.Students.AddAsync(std);
                await studentDb.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            Console.WriteLine(std);

            return View(std);
        }



        public async Task<IActionResult> Details(int id)
        {

            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);
            return View(stdData);
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
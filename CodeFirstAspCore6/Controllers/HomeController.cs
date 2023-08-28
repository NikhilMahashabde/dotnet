using CodeFirstAspCore6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstAspCore6.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext studentDb;

        public HomeController(StudentDbContext studentDb, ILogger<HomeController> logger)

        {
            this.studentDb = studentDb;
            _logger = logger;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDb.Students.AddAsync(std);
                await studentDb.SaveChangesAsync();
                TempData["insert_sucess"] = "Inserted...";
                return RedirectToAction("Index", "Home");
            }

            return View(std);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }

            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || studentDb.Students == null)
            {
                return NotFound();

            }

            var StudentData = await studentDb.Students.FindAsync(id);

            if (StudentData == null)
            {
                return NotFound();
            }

            Console.WriteLine(Request.Body);

            return View(StudentData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student stdForm)
        {
            if (id != stdForm.Id || id == null || studentDb.Students == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                studentDb.Students.Update(stdForm);
                await studentDb.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }

            return View();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }



            var student = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }

            var delData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (delData != null)
            {
                studentDb.Students.Remove(delData);
                await studentDb.SaveChangesAsync();

            }

            return RedirectToAction("Index", "Home");

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
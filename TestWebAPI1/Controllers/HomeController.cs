using Microsoft.AspNetCore.Mvc;

namespace TestWebAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HomeController : Controller
    {

        [HttpGet]
        public HomeData Get()
        {
            return new HomeData(Random.Shared.Next(20));

        }



    }
}

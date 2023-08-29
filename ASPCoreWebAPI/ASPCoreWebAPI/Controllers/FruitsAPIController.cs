using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {

        public List<string> fruits = new List<string>()
        {
            "apple",
            "orange",
            "mango"
        };

        [HttpGet]
        public List<string> getFruits()
        {
            return fruits;

        }

        [HttpGet("{id}")]
        public IActionResult getFruitsById(int id)
        {
            if (id < 0 || id >= fruits.Count)
            {
                return NotFound();
            }

            return Ok(fruits.ElementAt(id));

        }

    }
}

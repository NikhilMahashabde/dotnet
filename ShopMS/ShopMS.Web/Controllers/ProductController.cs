using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMS.Web.Models;
using ShopMS.Web.Services.IServices;

namespace ShopMS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductIndex()
        {

            List<ProductDto> list = new();
            var response = await _ProductService.GetAllProductsAsync<ResponseDto>();
            if (response != null && response.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMS.Web.Models;
using ShopMS.Web.Services.IServices;

namespace ShopMS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _ProductService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> ProductIndex()
        {

            List<ProductDto> list = new();
            var response = await _ProductService.GetAllProductsAsync<ResponseDto>();

            if (response != null && response.IsSuccess)
            {
                System.Diagnostics.Debug.WriteLine("Received products from API: {Products}", JsonConvert.SerializeObject(list));

                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);

        }

        public async Task<IActionResult> ProductCreate()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {

                var response = await _ProductService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);

        }

        public async Task<IActionResult> ProductEdit(int productId)
        {

            var response = await _ProductService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {

                var response = await _ProductService.UpdateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);

        }

        public async Task<IActionResult> ProductDelete(int productId)
        {

            var response = await _ProductService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(model);
            }


            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {

            System.Diagnostics.Debug.WriteLine("Deleted?");
            System.Diagnostics.Debug.WriteLine(model.ProductId);


            if (ModelState.IsValid)
            {

                System.Diagnostics.Debug.WriteLine("Deleted2?");
                System.Diagnostics.Debug.WriteLine(model.ProductId);

                var response = await _ProductService.DeleteProductAsync<ResponseDto>(model.ProductId);

                System.Diagnostics.Debug.WriteLine("Deleted?");
                System.Diagnostics.Debug.WriteLine(response.IsSuccess);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            else
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);

                }
                System.Diagnostics.Debug.WriteLine("Not found or not valid?");


            }
            return RedirectToAction(nameof(ProductIndex));
        }
    }
}

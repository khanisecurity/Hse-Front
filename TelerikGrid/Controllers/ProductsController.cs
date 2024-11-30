using Microsoft.AspNetCore.Mvc;
using TelerikGrid.DbContext;

namespace TelerikGrid.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // بارگذاری تمامی کالاها  
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        // بارگذاری واحدهای مرتبط با کالا  
        [HttpGet("{id}/units")]
        public IActionResult GetUnitsByProductId(int id)
        {
            var units =  _productService.GetUnitsByProductIdAsync(id);
            return Ok(units);
        }
    }
}
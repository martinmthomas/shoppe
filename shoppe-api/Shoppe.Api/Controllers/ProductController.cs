using Microsoft.AspNetCore.Mvc;
using Shoppe.Api.Services;

namespace Shoppe.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets list of products available for purchase.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Shoppe.Api.Models;
using Shoppe.Api.Services;

namespace Shoppe.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Gets user's cart.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult Get(Guid userId)
        {
            return Ok(_cartService.Get(userId));
        }

        /// <summary>
        /// Updates user's cart.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="products">Full list of products that form the Cart.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("{userId}")]
        public IActionResult Save([FromRoute] Guid userId, [FromBody] IEnumerable<Product> products)
        {
            var cart = _cartService.Save(userId, products);
            return Ok(cart);
        }
    }
}

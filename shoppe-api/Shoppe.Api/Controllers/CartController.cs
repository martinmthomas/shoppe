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
        public IActionResult Get(string userId)
        {
            return Ok(_cartService.Get(userId));
        }

        /// <summary>
        /// Updates user's cart.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost()]
        public IActionResult Save(CartUpdateRequest request)
        {
            var cart = _cartService.Save(request);
            return Ok(cart);
        }
    }
}

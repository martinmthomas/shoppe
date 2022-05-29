using Microsoft.AspNetCore.Mvc;
using Shoppe.Api.Models;
using Shoppe.Api.Services;

namespace Shoppe.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Places an order.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="products">List of products to be purchased</param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public IActionResult PlaceOrder([FromRoute] Guid userId, [FromBody] IEnumerable<Product> products)
        {
            var orderId = _orderService.PlaceOrder(userId, products);
            return Ok(new { OrderId = orderId });
        }
    }
}

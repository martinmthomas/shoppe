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
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult PlaceOrder(PlaceOrderRequest request)
        {
            var orderId = _orderService.PlaceOrder(request);
            return Ok(new { OrderId = orderId });
        }
    }
}

using Shoppe.Api.Models;

namespace Shoppe.Api.Services
{

    public interface IOrderService
    {
        string PlaceOrder(Guid userId, IEnumerable<Product> products);
    }

    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ICartService cartService, ILogger<OrderService> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public string PlaceOrder(Guid userId, IEnumerable<Product> products)
        {
            var orderId = Guid.NewGuid().ToString();
            _logger.LogInformation($"Order {orderId} placed successfully for user {userId}");

            _cartService.Clear(userId);
            _logger.LogInformation($"Cart successfully cleared for user {userId} after placing order.");

            return orderId;
        }
    }
}

using Shoppe.Api.Models;
using Shoppe.Api.Models.Validators;
using Shoppe.Api.Repositories;

namespace Shoppe.Api.Services
{

    public interface IOrderService
    {
        Guid PlaceOrder(Guid userId, IEnumerable<Product> products);
    }

    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ICartService cartService, ILogger<OrderService> logger, IProductService productService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _logger = logger;
            _productService = productService;
            _orderRepository = orderRepository;
        }

        public Guid PlaceOrder(Guid userId, IEnumerable<Product> products)
        {
            var productList = _productService.GetAll();

            // Updates MaxAvailable value in the request, based on latest list.
            var updatedProducts = Enumerable.Empty<Product>();
            foreach (var product in products)
            {
                var latestProduct = productList.First(p => p.Code == product.Code);
                updatedProducts = updatedProducts.Append(new Product(product.Code, product.Description, product.ImageUrl, product.Price, latestProduct.MaxAvailable, product.Quantity));
            }

            // Validates the request model using custom validator.
            var productsValidator = new ProductsValidator();
            var validationResult = productsValidator.Validate(updatedProducts);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.ToString("~"));

            // Saves order, updates product list and clears the cart.
            var orderId = _orderRepository.PlaceOrder(userId, updatedProducts);
            _logger.LogInformation($"Order {orderId} placed successfully for user {userId}");

            _productService.UpdateProductsAvailability(updatedProducts);
            _logger.LogInformation($"Products availability updated based on this order");

            _cartService.Clear(userId);
            _logger.LogInformation($"Cart successfully cleared for user {userId} after placing order.");

            return orderId;
        }
    }
}

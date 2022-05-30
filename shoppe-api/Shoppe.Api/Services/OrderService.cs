using Shoppe.Api.Models;
using Shoppe.Api.Repositories;
using Shoppe.Api.Validators;

namespace Shoppe.Api.Services
{

    public interface IOrderService
    {
        string PlaceOrder(PlaceOrderRequest request);
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

        public string PlaceOrder(PlaceOrderRequest request)
        {
            var updatedProducts = ValidateUsingLatestProductData(request.Products);

            // Saves order, updates product list and clears the cart.
            var updatedRequest = new PlaceOrderRequest(request.UserId, updatedProducts);
            var orderId = _orderRepository.PlaceOrder(updatedRequest);
            _logger.LogInformation($"Order {orderId} placed successfully for user {request.UserId}");

            _productService.UpdateProductsAvailability(updatedProducts);
            _logger.LogInformation($"Products availability updated based on this order");

            _cartService.Clear(request.UserId);
            _logger.LogInformation($"Cart successfully cleared for user {request.UserId} after placing order.");

            return orderId;
        }

        private IEnumerable<ProductSlim> ValidateUsingLatestProductData(IEnumerable<ProductSlim> products)
        {
            var productList = _productService.GetAll();

            // Updates MaxAvailable value, based on latest product list.
            var updatedProducts = Enumerable.Empty<ProductSlim>();
            foreach (var product in products)
            {
                var latestProduct = productList.First(p => p.Code == product.Code);

                var updatedProduct = new ProductSlim(product.Code, product.Price, latestProduct.MaxAvailable, product.Quantity);
                updatedProducts = updatedProducts.Append(updatedProduct);
            }

            // Validates the request using custom validator.
            var productSlimsValidator = new ProductSlimsValidator();
            var validationResult = productSlimsValidator.Validate(updatedProducts);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.ToString("~"));

            return updatedProducts;
        }
    }
}

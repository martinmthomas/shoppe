using Microsoft.Extensions.Logging;
using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Repositories;
using Shoppe.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Services
{
    public class OrderServiceTests
    {
        private IOrderService CreateSut(
            ICartService cartService = null,
            IProductService productService = null,
            IOrderRepository orderRepository = null)
        {
            var logger = new Mock<ILogger<OrderService>>();

            return new OrderService(cartService, logger.Object, productService, orderRepository);
        }

        [Fact]
        public void PlaceOrder_Should_Place_Order()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(o => o.PlaceOrder(It.IsAny<Guid>(), It.IsAny<IEnumerable<Product>>()));

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(p => p.UpdateProductsAvailability(It.IsAny<IEnumerable<Product>>()));

            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.Clear(It.Is<Guid>(u => u == userId)));

            // Act
            var sut = CreateSut(mockCartService.Object, mockProductService.Object, mockOrderRepository.Object);
            var orderId = sut.PlaceOrder(userId, Enumerable.Empty<Product>());

            // Assert
            mockOrderRepository.VerifyAll();
            mockProductService.VerifyAll();
            mockCartService.VerifyAll();
        }
    }
}

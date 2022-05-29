using Microsoft.Extensions.Logging;
using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Services;
using System;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Services
{
    public class OrderServiceTests
    {
        private IOrderService CreateSut(ICartService cartService = null)
        {
            var logger = new Mock<ILogger<OrderService>>();

            return new OrderService(cartService, logger.Object);
        }

        [Fact]
        public void PlaceOrder_Should_Clear_Cart()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.Clear(It.Is<Guid>(u => u == userId)));

            // Act
            var sut = CreateSut(mockCartService.Object);
            var orderId = sut.PlaceOrder(userId, Enumerable.Empty<Product>());

            // Assert
            mockCartService.VerifyAll();
            Assert.NotNull(orderId);
        }
    }
}

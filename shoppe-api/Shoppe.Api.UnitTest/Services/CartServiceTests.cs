using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Repositories;
using Shoppe.Api.Services;
using System.Collections.Generic;
using Xunit;

namespace Shoppe.Api.UnitTest.Services
{
    public class CartServiceTests
    {
        public ICartService CreateSut(ICartRepository cartRepository = null)
        {
            return new CartService(cartRepository);
        }

        [Fact]
        public void Clear_Should_Call_CartRepository()
        {
            // Arrange
            var userId = "User-123";

            var mockCartRepo = new Mock<ICartRepository>();
            mockCartRepo.Setup(p => p.Clear(It.Is<string>(u => u == userId)));

            // Act
            var sut = CreateSut(mockCartRepo.Object);
            sut.Clear(userId);

            // Assert
            mockCartRepo.VerifyAll();
        }

        [Fact]
        public void Get_Should_Call_CartRepository()
        {
            // Arrange
            var userId = "User-123";

            var mockCartRepo = new Mock<ICartRepository>();
            mockCartRepo.Setup(p => p.Get(It.Is<string>(u => u == userId)));

            // Act
            var sut = CreateSut(mockCartRepo.Object);
            sut.Get(userId);

            // Assert
            mockCartRepo.VerifyAll();
        }

        [Fact]
        public void Save_Should_Call_CartRepository()
        {
            // Arrange
            var request = new CartUpdateRequest("User-123", new List<ProductSlim>());

            var mockCartRepo = new Mock<ICartRepository>();
            mockCartRepo.Setup(p => p.Save(It.Is<CartUpdateRequest>(r => r == request)));

            // Act
            var sut = CreateSut(mockCartRepo.Object);
            sut.Save(request);

            // Assert
            mockCartRepo.VerifyAll();
        }
    }
}

using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Repositories;
using Shoppe.Api.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Services
{
    public class ProductServiceTests
    {
        public IProductService CreateSut(IProductRepository productRepository = null)
        {
            return new ProductService(productRepository);
        }

        [Fact]
        public void GetAll_Should_Call_ProductRepository_And_Return_ProductList()
        {
            // Arrange
            var expProductList = Enumerable.Empty<Product>()
                .Append(new Product("biscuit", "Biscuit 500g", "biscuit.jpg", 4.5f, 100, 1))
                .Append(new Product("bread", "Bread 750g", "bread.jpg", 5.0f, 100, 1))
                .Append(new Product("bun", "Buns 4 Pack", "brioche_burger_bun.jpg", 6.0f, 100, 2));

            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(p => p.GetAll()).Returns(expProductList);

            // Act
            var sut = CreateSut(mockProductRepo.Object);
            var productList = sut.GetAll();

            // Assert
            mockProductRepo.VerifyAll();
            Assert.Equal(expProductList, productList);
        }

        [Fact]
        public void UpdateProductsAvailability_Should_Call_ProductRepository()
        {
            // Arrange
            var latestProducts = Enumerable.Empty<ProductSlim>()
                .Append(new ProductSlim("biscuit", 4.5f, 100, 1))
                .Append(new ProductSlim("bread", 5.0f, 100, 1))
                .Append(new ProductSlim("bun", 6.0f, 100, 2));

            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(p => p.UpdateProductsAvailability(It.Is<IEnumerable<ProductSlim>>(p => p == latestProducts)));

            // Act
            var sut = CreateSut(mockProductRepo.Object);
            sut.UpdateProductsAvailability(latestProducts);

            // Assert
            mockProductRepo.VerifyAll();
        }
    }
}

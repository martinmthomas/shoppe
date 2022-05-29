using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Repositories
{
    public class CartRepositoryTests
    {
        public ICartRepository CreateSut(IMemoryCache cache = null)
        {
            var mockLogger = new Mock<ILogger<CartRepository>>();

            return new CartRepository(cache, mockLogger.Object);
        }

        [Fact]
        public void Get_Should_Return_Empty_Cart_When_No_Cart_Data_Can_Be_Found()
        {
            // Arrange
            var userId = Guid.NewGuid();
            IEnumerable<Product> expectedProducts = null;

            var cacheMock = new MockMemoryCache();
            cacheMock.SetupTryGetValue(userId, expectedProducts, false);

            // Act
            var sut = CreateSut(cacheMock);
            var cart = sut.Get(userId);

            // Assert
            Assert.NotNull(cart);
            Assert.Empty(cart.Products);
            Assert.Equal(0, cart.ShippingCost);
            Assert.Equal(0, cart.Total);
        }

        [Fact]
        public void Get_Should_Create_Cart_When_Products_Are_Found()
        {
            // Arrange
            var userId = Guid.NewGuid();
            IEnumerable<Product> expectedProducts = Enumerable.Empty<Product>()
                .Append(new Product("biscuit", "Biscuit 500g", "biscuit.jpg", 4.5f, 1))
                .Append(new Product("bread", "Bread 750g", "bread.jpg", 5.0f, 1))
                .Append(new Product("bun", "Buns 4 Pack", "brioche_burger_bun.jpg", 6.0f, 2));

            var cacheMock = new MockMemoryCache();
            cacheMock.SetupTryGetValue(userId, expectedProducts, true);

            // Act
            var sut = CreateSut(cacheMock);
            var cart = sut.Get(userId);

            // Assert
            Assert.NotNull(cart);
            Assert.Equal(3, cart.Products.Count());
            Assert.Equal(10, cart.ShippingCost);
            Assert.Equal(21.5, cart.Total);
        }
    }

    public class MockMemoryCache : IMemoryCache
    {
        private object _expKey;
        private object _expValue;
        private bool _expTryGetValueReturn;

        public void SetupTryGetValue(object expKey, object expValue, bool expReturn)
        {
            _expKey = expKey;
            _expValue = expValue;
            _expTryGetValueReturn = expReturn;
        }

        public bool TryGetValue(object key, out object value)
        {
            Assert.Equal(_expKey, key);

            value = _expValue;
            return _expTryGetValueReturn;
        }

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }
    }
}

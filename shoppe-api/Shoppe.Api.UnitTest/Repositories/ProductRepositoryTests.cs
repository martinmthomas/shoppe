using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Shoppe.Api.Models;
using Shoppe.Api.Models.Configs;
using Shoppe.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Repositories
{
    public class ProductRepositoryTests
    {
        private IProductRepository CreateSut(IOptions<CoreSettings> options = null, IMemoryCache cache = null)
        {
            options ??= Options.Create<CoreSettings>(new CoreSettings());

            return new ProductRepository(options, cache);
        }

        [Fact]
        public void UpdateProductsAvailability_Should_Update_MaxAvailability()
        {
            // Arrange
            var productList = Enumerable.Empty<Product>()
                .Append(new Product("biscuit", "Biscuit 500g", "biscuit.jpg", 4.5f, 100, 1))
                .Append(new Product("bread", "Bread 750g", "bread.jpg", 5.0f, 100, 1))
                .Append(new Product("bun", "Buns 4 Pack", "brioche_burger_bun.jpg", 6.0f, 100, 2));

            var latestProducts = Enumerable.Empty<Product>()
                .Append(new Product("biscuit", "Biscuit 500g", "biscuit.jpg", 4.5f, 40));

            var expectedProducts = Enumerable.Empty<Product>()
                .Append(new Product("biscuit", "Biscuit 500g", "biscuit.jpg", 4.5f, 40, 0))
                .Append(new Product("bread", "Bread 750g", "bread.jpg", 5.0f, 100, 0))
                .Append(new Product("bun", "Buns 4 Pack", "brioche_burger_bun.jpg", 6.0f, 100, 0));

            var cacheMock = new MockMemoryCache<IEnumerable<Product>>();
            cacheMock.SetupGet("PRODUCT_LIST", productList);
            cacheMock.SetupSet("PRODUCT_LIST", expectedProducts);

            // Act
            var sut = CreateSut(null, cacheMock);
            sut.UpdateProductsAvailability(latestProducts);

            // Asserts are already in the cache mock.
        }
        private class MockMemoryCache<TItem> : IMemoryCache
        {
            private object _expKeyGet;
            private TItem _expValueGet;

            private object _expKeySet;
            private TItem _expValueToSet;

            public void SetupGet(object expKeyGet, TItem expValueGet)
            {
                _expKeyGet = expKeyGet;
                _expValueGet = expValueGet;
            }

            public void SetupSet(object expKeySet, TItem expValueToSet)
            {
                _expKeySet = expKeySet;
                _expValueToSet = expValueToSet;
            }

            public TItem Get(object key)
            {
                Assert.Equal(_expKeyGet, key);
                return _expValueGet;
            }

            public TItem Set(object key, TItem value)
            {
                Assert.Equal(_expKeySet, key);
                Assert.Equal(_expValueToSet, value);
                return value;
            }

            public bool TryGetValue(object key, out object value)
            {
                value = Enumerable.Empty<Product>()
                    .Append(new Product("", "", "", 1, 3));

                return true;
            }

            public ICacheEntry CreateEntry(object key)
            {
                var mock = new Mock<ICacheEntry>();
                return mock.Object;
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
}
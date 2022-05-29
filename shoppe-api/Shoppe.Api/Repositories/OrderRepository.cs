using Microsoft.Extensions.Caching.Memory;
using Shoppe.Api.Models;

namespace Shoppe.Api.Repositories
{

    public interface IOrderRepository
    {
        Guid PlaceOrder(Guid userId, IEnumerable<Product> products);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IMemoryCache _cache;

        private const string _orderKey = "ORDERS";

        public OrderRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Guid PlaceOrder(Guid userId, IEnumerable<Product> products)
        {
            var order = new Order(Guid.NewGuid(), userId, products, DateTime.UtcNow);
            var cacheKey = $"{_orderKey}_{userId}";

            if (_cache.TryGetValue<IEnumerable<Order>>(_orderKey, out var orders))
            {
                _cache.Set(cacheKey, orders.Append(order));
            }
            else
            {
                _cache.Set(cacheKey, Enumerable.Empty<Order>().Append(order));
            }

            return order.OrderId;
        }
    }
}

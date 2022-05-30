using Microsoft.Extensions.Caching.Memory;
using Shoppe.Api.Models;

namespace Shoppe.Api.Repositories
{

    public interface IOrderRepository
    {
        string PlaceOrder(PlaceOrderRequest request);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IMemoryCache _cache;

        private const string _orderKey = "ORDERS";

        public OrderRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string PlaceOrder(PlaceOrderRequest request)
        {
            var order = new Order(request);

            var cacheKey = $"{_orderKey}_{order.UserId}";

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

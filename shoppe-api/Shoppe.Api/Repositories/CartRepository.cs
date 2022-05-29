using Microsoft.Extensions.Caching.Memory;
using Shoppe.Api.Models;

namespace Shoppe.Api.Repositories
{
    public interface ICartRepository
    {
        /// <summary>
        /// Gets any pre-saved products and returns the Cart for the given userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Cart Get(Guid userId);

        /// <summary>
        /// Saves selected products in an "Inmemory implementation of Cart" for the given userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Cart Save(Guid userId, IEnumerable<Product> products);

        /// <summary>
        /// Clears user's Cart.
        /// </summary>
        /// <param name="userId"></param>
        void Clear(Guid userId);
    }

    public class CartRepository : ICartRepository
    {
        /// <summary>
        /// Using memory cache as a datastore for a simple implementation. This should be replaced by a proper database.
        /// </summary>
        private readonly IMemoryCache _cache;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(IMemoryCache cache, ILogger<CartRepository> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Cart Get(Guid userId)
        {
            if (_cache.TryGetValue<IEnumerable<Product>>(userId, out var products))
            {
                return new Cart { Products = products };
            }

            _logger.LogInformation($"Cart does not exist for user {userId}. So, creating a new one.");

            return new Cart();
        }

        public Cart Save(Guid userId, IEnumerable<Product> products)
        {
            _cache.Set(userId, products);
            return new Cart { Products = products };
        }

        public void Clear(Guid userId)
        {
            _cache.Remove(userId);
        }
    }
}

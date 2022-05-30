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
        Cart Get(string userId);

        /// <summary>
        /// Saves selected products in an "Inmemory implementation of Cart" for the given userId.
        /// </summary>
        /// <returns></returns>
        Cart Save(CartUpdateRequest request);

        /// <summary>
        /// Clears user's Cart.
        /// </summary>
        /// <param name="userId"></param>
        void Clear(string userId);
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

        public Cart Get(string userId)
        {
            if (_cache.TryGetValue<IEnumerable<ProductSlim>>(userId, out var products))
            {
                return new Cart { Products = products };
            }

            _logger.LogInformation($"Cart does not exist for user {userId}. So, returning an empty one.");

            return new Cart();
        }

        public Cart Save(CartUpdateRequest request)
        {
            _cache.Set(request.UserId, request.Products);
            return new Cart { Products = request.Products };
        }

        public void Clear(string userId)
        {
            _cache.Remove(userId);
        }
    }
}

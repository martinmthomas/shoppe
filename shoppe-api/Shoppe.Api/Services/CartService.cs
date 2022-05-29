using Shoppe.Api.Models;
using Shoppe.Api.Repositories;

namespace Shoppe.Api.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Gets user's Cart with any pre-selected products.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Cart Get(Guid userId);

        /// <summary>
        /// Saves selected products in the user's Cart.
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

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void Clear(Guid userId)
        {
            _cartRepository.Clear(userId);
        }

        public Cart Get(Guid userId)
        {
            return _cartRepository.Get(userId);
        }

        public Cart Save(Guid userId, IEnumerable<Product> products)
        {
            return _cartRepository.Save(userId, products);
        }
    }
}

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
        Cart Get(string userId);

        /// <summary>
        /// Saves selected products in the user's Cart.
        /// </summary>
        /// <returns></returns>
        Cart Save(CartUpdateRequest request);

        /// <summary>
        /// Clears user's Cart.
        /// </summary>
        /// <param name="userId"></param>
        void Clear(string userId);
    }

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void Clear(string userId)
        {
            _cartRepository.Clear(userId);
        }

        public Cart Get(string userId)
        {
            return _cartRepository.Get(userId);
        }

        public Cart Save(CartUpdateRequest request)
        {
            return _cartRepository.Save(request);
        }
    }
}

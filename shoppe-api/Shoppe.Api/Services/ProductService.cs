using Shoppe.Api.Models;
using Shoppe.Api.Repositories;

namespace Shoppe.Api.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Gets all products and their details.
        /// </summary>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// Updates products availability.
        /// </summary>
        /// <param name="latestProducts"></param>
        void UpdateProductsAvailability(IEnumerable<Product> latestProducts);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public void UpdateProductsAvailability(IEnumerable<Product> latestProducts)
        {
            _productRepository.UpdateProductsAvailability(latestProducts);
        }
    }
}

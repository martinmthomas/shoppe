using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shoppe.Api.Models;
using Shoppe.Api.Models.Configs;

namespace Shoppe.Api.Repositories
{

    public interface IProductRepository
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

    public class ProductRepository : IProductRepository
    {
        private readonly CoreSettings _coreSettings;
        private readonly IMemoryCache _cache;

        private const string _productListKey = "PRODUCT_LIST";

        public ProductRepository(IOptions<CoreSettings> options, IMemoryCache cache)
        {
            _coreSettings = options.Value;
            _cache = cache;
            LoadProductList();
        }

        public IEnumerable<Product> GetAll()
        {
            return _cache.Get<IEnumerable<Product>>(_productListKey);
        }

        public void UpdateProductsAvailability(IEnumerable<Product> latestProducts)
        {
            var products = GetAll();

            var updatedProducts = Enumerable.Empty<Product>();
            foreach (var product in products)
            {
                if (latestProducts.Any(p => p.Code == product.Code))
                {
                    var latestProduct = latestProducts.First(p => p.Code == product.Code);
                    var max = latestProduct.MaxAvailable - latestProduct.Quantity;
                    updatedProducts = updatedProducts.Append(new Product(product.Code, product.Description, product.ImageUrl, product.Price, max));
                }
                else
                {
                    updatedProducts = updatedProducts.Append(product);
                }
            }

            _cache.Set(_productListKey, updatedProducts);
        }

        private void LoadProductList()
        {
            if (!_cache.TryGetValue<IEnumerable<Product>>(_productListKey, out var listInCache) || !listInCache.Any())
            {
                IEnumerable<Product> productList = Enumerable.Empty<Product>()
                    .Append(new Product("arnott_biscuit_1", "Arnott's Assorted Biscuits Pack 500g", $"{_coreSettings.AssetsUrl}biscuit.jpg", 4.5f, 20))
                    .Append(new Product("helga_bread_1", "Helga's Bread Traditional Wholemeal 750g", $"{_coreSettings.AssetsUrl}bread.jpg", 5.0f, 50))
                    .Append(new Product("brioche_bun_1", "Brioche Gourmet Burger Buns 4 Pack", $"{_coreSettings.AssetsUrl}brioche_burger_bun.jpg", 6.0f, 100))
                    .Append(new Product("a2_milk_2", "A2 Full cream milk 2L", $"{_coreSettings.AssetsUrl}milk.jpg", 3.5f, 200))
                    .Append(new Product("bega_peanut_1", "Bega Peanut Butter Crunchy 780g", $"{_coreSettings.AssetsUrl}peanut_butter.jpg", 5.5f, 500));
                _cache.Set(_productListKey, productList);
            }
        }
    }
}

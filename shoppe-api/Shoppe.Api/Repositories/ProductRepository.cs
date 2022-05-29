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
    }

    public class ProductRepository : IProductRepository
    {
        private readonly CoreSettings _coreSettings;

        public ProductRepository(IOptions<CoreSettings> options)
        {
            _coreSettings = options.Value;
        }

        public IEnumerable<Product> GetAll()
        {
            return Enumerable.Empty<Product>()
                .Append(new Product("arnott_biscuit_1", "Arnott's Assorted Biscuits Pack 500g", $"{_coreSettings.AssetsUrl}biscuit.jpg", 4.5f))
                .Append(new Product("helga_bread_1", "Helga's Bread Traditional Wholemeal 750g", $"{_coreSettings.AssetsUrl}bread.jpg", 5.0f))
                .Append(new Product("brioche_bun_1", "Brioche Gourmet Burger Buns 4 Pack", $"{_coreSettings.AssetsUrl}brioche_burger_bun.jpg", 6.0f))
                .Append(new Product("a2_milk_2", "A2 Full cream milk 2L", $"{_coreSettings.AssetsUrl}milk.jpg", 3.5f))
                .Append(new Product("bega_peanut_1", "Bega Peanut Butter Crunchy 780g", $"{_coreSettings.AssetsUrl}peanut_butter.jpg", 5.5f));
        }
    }
}

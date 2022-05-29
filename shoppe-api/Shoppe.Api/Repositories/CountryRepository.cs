using Shoppe.Api.Models;

namespace Shoppe.Api.Repositories
{

    public interface ICountryRepository
    {
        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> GetAll();
    }

    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<Country> GetAll()
        {
            return Enumerable.Empty<Country>()
                .Append(new Country("au", "Australia", "$", 1f))
                .Append(new Country("ind", "India", "₹", 55.5f))
                .Append(new Country("gb", "United Kingdom", "£", 0.57f));
        }
    }
}

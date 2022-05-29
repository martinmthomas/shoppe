using Shoppe.Api.Models;
using Shoppe.Api.Repositories;

namespace Shoppe.Api.Services
{
    public interface ICountryService
    {
        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> GetAll();
    }

    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IEnumerable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }
    }
}

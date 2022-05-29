using Microsoft.AspNetCore.Mvc;
using Shoppe.Api.Services;

namespace Shoppe.Api.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Gets list of countries.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var countries = _countryService.GetAll();
            return Ok(countries);
        }
    }
}

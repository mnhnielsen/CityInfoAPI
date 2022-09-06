using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly CitiesDataStore _cityDataStore;

        public CitiesController(ILogger<CitiesController> logger, CitiesDataStore cityDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityDataStore = cityDataStore ?? throw new ArgumentNullException(nameof(cityDataStore));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var cities = _cityDataStore.Cities;
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = _cityDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                _logger.LogInformation($"City with id {id} wasn't found when accessing the city.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}

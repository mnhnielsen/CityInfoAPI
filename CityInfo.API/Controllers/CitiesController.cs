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

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var cities = CitiesDataStore.Current.Cities;
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                _logger.LogInformation($"City with id {id} wasn't found when accessing the city.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}

using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex, DateTime.UtcNow.ToLongTimeString());
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterest == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {


            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            //demo purposes - to be improved
            var id = _citiesDataStore.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                 new
                 {
                     cityId = cityId,
                     pointOfInterestId = finalPointOfInterest.Id
                 },
                 finalPointOfInterest);
        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestToUpdate = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestToUpdate == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            pointOfInterestToUpdate.Name = pointOfInterest.Name;
            pointOfInterestToUpdate.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(pointOfInterestToPatch))
                return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            _localMailService.Send("Point of interested deleted.",
                $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted.");
            return NoContent();
        }
    }
}

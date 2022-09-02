using CityInfo.API.Models;
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

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

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


            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            //demo purposes - to be improved
            var id = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointsOfInterests.Add(finalPointOfInterest);

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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestToUpdate = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found when accessing points of interest.", DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }

            city.PointsOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();
        }
    }
}

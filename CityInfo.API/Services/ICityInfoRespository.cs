using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        //Async - freeing up threads asap for other tasks. Improves scalability.
        Task<IEnumerable<City>> GetCitiesAsync();

        //Nullable with ?
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
    }
}

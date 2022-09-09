using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRespository
    {
        //Async - freeing up threads asap for other tasks. Improves scalability.
        Task<IEnumerable<City>> GetCitiesAsync();

        //Nullable with ?
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterst);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId, int pointOfInterstId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId);
    }
}

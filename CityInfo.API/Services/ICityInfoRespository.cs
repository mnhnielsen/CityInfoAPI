using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRespository
    {
        //Async - freeing up threads asap for other tasks. Improves scalability.
        Task<IEnumerable<City>> GetCitiesAsync();

        //Nullable with ?
        Task<City?> GetCityAsync(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
    }
}

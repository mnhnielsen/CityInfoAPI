using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        //Async - freeing up threads asap for other tasks. Improves scalability.
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery);

        //Nullable with ?
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);

        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterst);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);    //In memory operation. No async needed.

    }
}

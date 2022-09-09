using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRespository
    {
        //Async - freeing up threads asap for other tasks. Improves scalability.
        Task<IEnumerable<City>> GetCitiesAsync();
    }
}

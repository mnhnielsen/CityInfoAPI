using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with the big park"
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Berlin",
                    Description = "The one with the nice kebabs"
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Lucca",
                    Description = "The one with the fast sprinter"
                }
            };
        }

    }
}

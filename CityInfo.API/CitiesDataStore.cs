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
                    Description = "The one with the big park",
                    PoinstOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "A big park in a city"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Jerry's Apartment",
                            Description = "This is were Jerry lives"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Berlin",
                    Description = "The one with the nice kebabs",
                    PoinstOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Checkpoint Charlie",
                            Description = "Berlin Wall checkpoint"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Zaddy's",
                            Description = "Cheap and good kebab place"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Lucca",
                    Description = "The one with the fast sprinter",
                    PoinstOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "City Walls",
                            Description = "The wall around the city"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Hills",
                            Description = "Hills/mountains overlooking the coast"
                        }
                    }
                }
            };
        }

    }
}

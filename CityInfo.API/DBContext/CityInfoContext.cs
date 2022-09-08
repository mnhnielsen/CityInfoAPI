using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DBContext
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New York")
                {
                    Id = 1,
                    Description = "The one with that big park"
                },
                new City("Berlin")
                {
                    Id = 2,
                    Description = "The one with the good kebab"
                },
                new City("London")
                {
                    Id = 3,
                    Description = "The one with that big tower with the clock"
                }
                );

            modelBuilder.Entity<PointOfInterest>().
                HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in the United States."
                },
                new PointOfInterest("Checkpoint Charlie")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "The most famous Berlin Wall checkpoints from the Cold War."
                },
                new PointOfInterest("New York Stock Exchange")
                {
                    Id = 3,
                    CityId = 1,
                    Description = "Many of the biggest and well known companies are traded publicly on this exchange."
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return PoinstOfInterests.Count;
            }
        }
        public ICollection<PointOfInterestDto> PoinstOfInterests { get; set; } = new List<PointOfInterestDto>();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<Object>
                {
                    new { id = 1, Name= "Odense"},
                    new { id = 2, Name= "Berlin"}
                });
        }
    }
}

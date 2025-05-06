using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SmartCities.Api.Controllers
{
    public record City(int Id, string Name);

    [ApiController]
    [Route("cities")]
    public class CitiesController : ControllerBase
    {
        private static readonly List<City> Cities = new()
        {
            new City(1, "São Paulo"),
            new City(2, "Rio de Janeiro"),
            new City(3, "Salvador"),
            new City(4, "Belo Horizonte"),   // ⬅️ aqui
            new City(5, "Curitiba")
        };

        [HttpGet]
        public IActionResult GetAll() =>
            Ok(Cities);

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var city = Cities.FirstOrDefault(c => c.Id == id);
            return city is not null ? Ok(city) : NotFound();
        }
    }
}

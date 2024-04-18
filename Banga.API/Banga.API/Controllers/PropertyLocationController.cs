using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyLocationController : ControllerBase
    {
        private readonly IPropertyLocationService _propertyLocationService; 
        public PropertyLocationController(IPropertyLocationService propertyLocationService)
        {
            _propertyLocationService = propertyLocationService;
        }

        [HttpGet("Cities")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var data = await _propertyLocationService.GetCities();

            return Ok(data);
        }

        [HttpGet("Suburbs")]
        public async Task<ActionResult<IEnumerable<Suburb>>> GetSuburbs()
        {
            var data = await _propertyLocationService.GetSuburbs();

            return Ok(data);
        }

        [HttpGet("CitySuburbs")]
        public async Task<ActionResult<IEnumerable<string>>> GetCitySuburbs()
        {
            var data = await _propertyLocationService.GetCitySuburbs();

            return Ok(data);
        }

    }
}

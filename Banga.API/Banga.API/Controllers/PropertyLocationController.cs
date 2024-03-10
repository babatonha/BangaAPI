using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyLocationController : ControllerBase
    {
        private readonly IPropertyLocationService _propertyLocationService; 
        public PropertyLocationController(IPropertyLocationService propertyLocationService)
        {
            _propertyLocationService = propertyLocationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var data = await _propertyLocationService.GetCities();

            return Ok(data);
        }
    }
}

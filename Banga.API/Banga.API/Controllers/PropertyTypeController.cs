using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTypeController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService; 
        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyType>>> Get()
        {
            var data = await _propertyTypeService.GetPropertyTypes();

            return Ok(data);
        }
    }
}

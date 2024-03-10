using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;  
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService; 
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Property>>> Get() 
        {
            var properties = await _propertyService.GetProperties();

            if (!properties.Any())
            {
                return NotFound();
            }

            return Ok(properties);    
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateProperty([FromBody] Property property)
        {
            if(property.PropertyId >0) 
            { 
                 await _propertyService.UpdateProperty(property);
                return Ok(property.PropertyId);
            }
            var propertyId = await _propertyService.CreateProperty(property);

            return Ok(propertyId);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<VwProperty>>> GetPropertyById(long propertyId)
        {
            var property = await _propertyService.GetPropertyDetailsById(propertyId);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
    }
}

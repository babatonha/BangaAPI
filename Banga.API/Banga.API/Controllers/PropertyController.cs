using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

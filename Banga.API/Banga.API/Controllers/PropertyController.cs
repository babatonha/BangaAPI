using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("FilteredSearch")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatedList>> Get([FromBody] SearchFilterDTO searchFilter) 
        {
            var properties = await _propertyService.GetProperties(searchFilter, searchFilter.PageIndex,  searchFilter.PageSize);

            if (!properties.Items.Any())
            {
                return NotFound();
            }

            return Ok(properties);    
        }

        [HttpPost("Owner/{ownerId}")]
        [Authorize]
        public async Task<ActionResult<PaginatedList>> GetOwnerProperties([FromBody] SearchFilterDTO searchFilter, int ownerId)
        {
            var properties = await _propertyService.GetPropertiesByOwnerId(ownerId,
                searchFilter.PageIndex, searchFilter.PageSize, searchFilter.SearchTerms.Length > 0 ? searchFilter.SearchTerms[0] : "");

            if (!properties.Items.Any())
            {
                return NotFound();
            }

            return Ok(properties);
        }

        [HttpPost]
        [Authorize]
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<VwProperty>>> GetPropertyById(long propertyId)
        {
            var property = await _propertyService.GetPropertyDetailsById(propertyId);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }


        [HttpPut("Manage")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Property>>> ManageProperty([FromBody] ManagePropertyDTO manageProperty)
        {
            await _propertyService.ManageProperty(manageProperty);
            return Ok();
        }

        [HttpGet("Lookup")]
        [Authorize]
        public async Task<ActionResult<PropertyLookupData>> GetLookupData()
        {
            var data =  await _propertyService.GetPropertyLookupData();
            return Ok(data);
        }

    }
}

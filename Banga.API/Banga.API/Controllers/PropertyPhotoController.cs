using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PropertyPhotoController : ControllerBase
    {
        private readonly IPropertyPhotoService _propertyPhotoService;   
        public PropertyPhotoController(IPropertyPhotoService propertyPhotoService)
        {
            _propertyPhotoService = propertyPhotoService;   
        }

        [HttpPost("Upload/{propertyId}")]
        [Authorize]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, long propertyId)
        {
            if (!files.Any())
            {
                return BadRequest();
            }

            await _propertyPhotoService.UploadPhotos(files, propertyId);

            return Ok();
        }

        [HttpGet("{propertyId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropertyPhoto>>> GetPropertyPhotosByPropertyId(long propertyId)
        {
            var photos = await _propertyPhotoService.GetPropertyPhotosByPropertyId(propertyId);
            return Ok(photos);  
        }


        [HttpDelete("{photoId}")]
        [Authorize]
        public async Task<ActionResult> DeletePhoto(long photoId)
        {
            await _propertyPhotoService.DeletePhoto(photoId);
            return Ok();
        }
    }
}

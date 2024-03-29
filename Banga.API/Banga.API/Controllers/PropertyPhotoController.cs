﻿using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyPhotoController : ControllerBase
    {
        private readonly IPropertyPhotoService _propertyPhotoService;   
        public PropertyPhotoController(IPropertyPhotoService propertyPhotoService)
        {
            _propertyPhotoService = propertyPhotoService;   
        }

        [HttpPost("Upload/{propertyId}")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, long propertyId)
        {
            if (!files.Any())
            {
                return BadRequest("No files were provided.");
            }

            await _propertyPhotoService.UploadPhotos(files, propertyId);

            return Ok("Files uploaded successfully.");
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyPhoto>>> GetPropertyPhotosByPropertyId(long propertyId)
        {
            var photos = await _propertyPhotoService.GetPropertyPhotosByPropertyId(propertyId);
            return Ok(photos);  
        }
    }
}

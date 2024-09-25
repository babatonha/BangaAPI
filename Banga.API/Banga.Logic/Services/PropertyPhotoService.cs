using Banga.Data.Models;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Banga.Logic.Services
{
    public class PropertyPhotoService : IPropertyPhotoService
    {
        private readonly ICloudinaryPhotoService _cloudinaryPhotoService;
        private readonly IPropertyPhotoRepository _propertyPhotoRepository;
        private readonly IPropertyRepository _propertyRepository;

        public PropertyPhotoService(ICloudinaryPhotoService cloudinaryPhotoService,
            IPropertyRepository propertyRepository,
            IPropertyPhotoRepository propertyPhotoRepository)
        {
            _cloudinaryPhotoService = cloudinaryPhotoService;
            _propertyRepository = propertyRepository;
            _propertyPhotoRepository = propertyPhotoRepository;
        }

        public async Task DeletePhoto(long propertyPhotoId)
        {
            var photo = await _propertyPhotoRepository.GetPropertyPhotoById(propertyPhotoId);

            if(photo != null && photo.PublicID != null)
            {
                await _cloudinaryPhotoService.DeletePhotoAsync(photo.PublicID);      
            }

            await _propertyPhotoRepository.DeletePropertyPhoto(propertyPhotoId);
        }

        public Task<PropertyPhoto> GetPropertyPhotoById(long propertyPhotoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId)
        {
            return _propertyPhotoRepository.GetPropertyPhotosByPropertyId(propertyId);
        }

        public async Task UploadPhotos(List<IFormFile> files, long propertyId)
        {
            var thumbnailUrl = "";
            foreach (var file in files)
            {
                var cloudinaryPhoto = await _cloudinaryPhotoService.AddPhotoAsync(file);

                var photo = new PropertyPhoto
                {
                    PropertyId = propertyId,
                    PhotoUrl = cloudinaryPhoto.Url.ToString(),
                    PublicID = cloudinaryPhoto.PublicId,
                    IsMainPhoto = false,
                };

                await _propertyPhotoRepository.CreatePropertyPhoto(photo);

                if(thumbnailUrl=="")
                {
                    thumbnailUrl = photo.PhotoUrl;
                }
            }

            var property =  await _propertyRepository.GetPropertyById(propertyId);

            if(property != null) 
            {
                property.ThumbnailUrl = thumbnailUrl;
                await _propertyRepository.UpdateProperty(property);
            }
        }
    }
}

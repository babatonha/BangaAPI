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

        public PropertyPhotoService(ICloudinaryPhotoService cloudinaryPhotoService, IPropertyPhotoRepository propertyPhotoRepository)
        {
            _cloudinaryPhotoService = cloudinaryPhotoService;
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
            }
        }
    }
}

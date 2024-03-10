using Banga.Data.Models;
using Microsoft.AspNetCore.Http;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyPhotoService
    {
        Task UploadPhotos(List<IFormFile> files, long propertyId);
        Task DeletePhoto(long propertyPhotoId);
        Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId);
    }
}

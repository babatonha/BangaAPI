using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Banga.Domain.Interfaces.Services
{
    public interface ICloudinaryPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}

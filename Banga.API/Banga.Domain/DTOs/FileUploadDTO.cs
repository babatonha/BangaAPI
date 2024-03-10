using Microsoft.AspNetCore.Http;

namespace Banga.Domain.DTOs
{
    public class FileUploadDTO
    {
        public IFormFile FileDetails { get; set; } 
    }
}

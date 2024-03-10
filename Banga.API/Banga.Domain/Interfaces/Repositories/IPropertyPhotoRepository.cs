using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyPhotoRepository
    {
        Task<long> CreatePropertyPhoto(PropertyPhoto photo);
        Task UpdatePropertyPhoto(PropertyPhoto photo);
        Task DeletePropertyPhoto(long propertyPhotoId);
        Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId);
    }
}

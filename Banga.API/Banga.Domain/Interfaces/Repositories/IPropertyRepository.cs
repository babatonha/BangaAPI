using Banga.Data.Models;
using Banga.Data.ViewModels;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        Task<long> CreateProperty(Property property);
        Task<IEnumerable<Property>> GetProperties();
        Task<Property> GetPropertyById(long propertyId);
        Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId);
        Task<IEnumerable<PropertyOffer>> GetPropertyOffersByPropertyId(long propertyId);
    }
}

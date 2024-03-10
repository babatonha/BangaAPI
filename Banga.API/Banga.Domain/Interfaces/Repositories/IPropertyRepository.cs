using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task<IEnumerable<Property>> GetProperties();
        Task<Property> GetPropertyById(long propertyId);
        Task<IEnumerable<PropertyOffer>> GetPropertyOffersByPropertyId(long propertyId);
    }
}

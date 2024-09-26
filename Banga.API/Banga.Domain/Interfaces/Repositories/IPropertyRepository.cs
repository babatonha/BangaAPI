using Banga.Data.Models;
using Banga.Domain.DTOs;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter);
        Task<IEnumerable<Property>> GetPropertiesByOwnerId(int ownerId, string[] searchTerms);
        Task<Property> GetPropertyById(long propertyId);
        Task ManageProperty(ManagePropertyDTO manage);

    }
}

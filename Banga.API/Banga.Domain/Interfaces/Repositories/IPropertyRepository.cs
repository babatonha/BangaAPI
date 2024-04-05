using Banga.Data.Models;
using Banga.Domain.DTOs;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter);
        Task<Property> GetPropertyById(long propertyId);
        Task ManageProperty(ManagePropertyDTO manage);

    }
}

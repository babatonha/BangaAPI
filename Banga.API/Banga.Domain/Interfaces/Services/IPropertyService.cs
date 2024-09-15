using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter);
        Task<VwProperty> GetPropertyDetailsById(long propertyId);
        Task<IEnumerable<Property>> GetPropertiesByOwnerId(int ownerId);
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task ManageProperty(ManagePropertyDTO manage);
        Task<PropertyLookupData> GetPropertyLookupData();
    }
}

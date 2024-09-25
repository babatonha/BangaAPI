using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<PaginatedList> GetProperties(SearchFilterDTO searchFilter, int pageIndex, int pageSize);
        Task<VwProperty> GetPropertyDetailsById(long propertyId);
        Task<PaginatedList> GetPropertiesByOwnerId(int ownerId, int pageIndex, int pageSize, string searchTerms);
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);
        Task ManageProperty(ManagePropertyDTO manage);
        Task<PropertyLookupData> GetPropertyLookupData();
    }
}

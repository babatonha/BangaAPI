using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter);
        Task<VwProperty> GetPropertyDetailsById(long propertyId);
        Task<long> CreateProperty(Property property);
        Task UpdateProperty(Property property);

    }
}

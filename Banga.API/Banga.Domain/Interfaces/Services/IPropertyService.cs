using Banga.Data.Models;
using Banga.Data.ViewModels;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<VwProperty> GetPropertyDetailsById(long propertyId); 
    }
}

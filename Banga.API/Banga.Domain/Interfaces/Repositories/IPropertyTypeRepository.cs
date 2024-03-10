using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypes();   
    }
}

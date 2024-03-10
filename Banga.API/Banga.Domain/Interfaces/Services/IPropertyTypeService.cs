using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypes();
    }
}

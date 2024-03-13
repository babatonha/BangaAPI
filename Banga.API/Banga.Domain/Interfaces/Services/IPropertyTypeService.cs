using Banga.Data.Models;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypes();
        Task<IEnumerable<RegistrationType>> GetPropertyRegistrationTypes();
    }
}

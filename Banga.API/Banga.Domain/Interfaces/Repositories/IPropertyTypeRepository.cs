using Banga.Data.Models;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypes();
        Task<IEnumerable<RegistrationType>> GetPropertyRegistrationTypes();
    }
}

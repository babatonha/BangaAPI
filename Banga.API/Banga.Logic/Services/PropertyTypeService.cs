using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;

        }

        public Task<IEnumerable<RegistrationType>> GetPropertyRegistrationTypes()
        {
            return _propertyTypeRepository.GetPropertyRegistrationTypes();
        }

        public Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            return _propertyTypeRepository.GetPropertyTypes();
        }
    }
}

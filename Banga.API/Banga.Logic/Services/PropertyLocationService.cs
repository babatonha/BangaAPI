using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class PropertyLocationService: IPropertyLocationService
    {
        private readonly IPropertyLocationRepository _propertyLocationRepository;   
        public PropertyLocationService(IPropertyLocationRepository propertyLocationRepository)
        {
            _propertyLocationRepository = propertyLocationRepository;   
        }

        public Task<IEnumerable<City>> GetCities()
        {
            return _propertyLocationRepository.GetCities();
        }

        public Task<IEnumerable<string>> GetCitySuburbs()
        {
            return _propertyLocationRepository.GetCitySuburbs();    
        }

        public Task<IEnumerable<Suburb>> GetSuburbs()
        {
            return _propertyLocationRepository.GetSuburbs();
        }
    }
}

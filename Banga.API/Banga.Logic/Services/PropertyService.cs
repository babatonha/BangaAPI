using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;

namespace Banga.Logic.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;    
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;   
        }

        public async Task<VwProperty> GetPropertyDetailsById(long propertyId)
        {
            var property = await _propertyRepository.GetPropertyById(propertyId);
            var photos = await _propertyRepository.GetPropertyPhotosByPropertyId(propertyId);
            var offers = await _propertyRepository.GetPropertyOffersByPropertyId(propertyId);


            return new VwProperty
            {
                Property = property,
                PropertyOffers = offers,
                PropertyPhotos = photos
            };
        }

        public Task<IEnumerable<Property>> GetProperties()
        {
            return _propertyRepository.GetProperties();
        }
    }
}

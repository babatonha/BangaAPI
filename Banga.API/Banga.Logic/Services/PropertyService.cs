using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;

namespace Banga.Logic.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyPhotoRepository _propertyPhotoRepository;
        private readonly IPropertyOfferRepository _propertyOfferRepository;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyPhotoRepository propertyPhotoRepository,
            IPropertyOfferRepository propertyOfferRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyPhotoRepository = propertyPhotoRepository;
            _propertyOfferRepository = propertyOfferRepository; 
        }

        public async Task<VwProperty> GetPropertyDetailsById(long propertyId)
        {
            var property =  _propertyRepository.GetPropertyById(propertyId);
            var photos = _propertyPhotoRepository.GetPropertyPhotosByPropertyId(propertyId);
            var offers = _propertyOfferRepository.GetOffersByPropertyId(propertyId);

            await Task.WhenAll(property, photos, offers);

            return new VwProperty
            {
                Property = property.Result,
                PropertyOffers = offers.Result,
                PropertyPhotos = photos.Result
            };
        }

        public Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter)
        {
            return _propertyRepository.GetProperties(searchFilter);
        }

        public Task<long> CreateProperty(Property property)
        {
            return _propertyRepository.CreateProperty(property);
        }

        public Task UpdateProperty(Property property)
        {
            return _propertyRepository.UpdateProperty(property);    
        }

    }
}

using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyPhotoRepository _propertyPhotoRepository;
        private readonly IPropertyOfferRepository _propertyOfferRepository;
        private readonly IPropertyLocationRepository _propertyLocationRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly ILawFirmRepository _lawfirmRepository;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyPhotoRepository propertyPhotoRepository,
            IPropertyOfferRepository propertyOfferRepository, IPropertyLocationRepository propertyLocationRepository,
            IPropertyTypeRepository propertyTypeRepository, ILawFirmRepository lawFirmRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyPhotoRepository = propertyPhotoRepository;
            _propertyOfferRepository = propertyOfferRepository; 
            _propertyLocationRepository = propertyLocationRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _lawfirmRepository = lawFirmRepository;
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

        public Task ManageProperty(ManagePropertyDTO manage)
        {
            return _propertyRepository.ManageProperty(manage);
        }

        public async Task<PropertyLookupData> GetPropertyLookupData()
        {
            var citiesTask = _propertyLocationRepository.GetCities();
            var suburbTask = _propertyLocationRepository.GetSuburbs();
            var propertyTypesTask = _propertyTypeRepository.GetPropertyTypes();
            var registrationTypesTask = _propertyTypeRepository.GetPropertyRegistrationTypes(); 
            var lawFirmTask = _lawfirmRepository.GetLawFirms();

            await Task.WhenAll(citiesTask, suburbTask, propertyTypesTask,  registrationTypesTask, lawFirmTask);

            return new PropertyLookupData
            {
                Cities = citiesTask.Result,
                Suburbs = suburbTask.Result,
                RegistrationTypes = registrationTypesTask.Result,
                PropertyTypes = propertyTypesTask.Result,
                LawFirms = lawFirmTask.Result
            };
        }
    }
}

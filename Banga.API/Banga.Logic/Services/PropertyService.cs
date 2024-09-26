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
            var property = _propertyRepository.GetPropertyById(propertyId);
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

        public async Task<PaginatedList> GetProperties(SearchFilterDTO searchFilter, int pageIndex, int pageSize)
        {
            var properties = await _propertyRepository.GetProperties(searchFilter);

            return await PaginateResult(properties, pageIndex, pageSize);
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

            await Task.WhenAll(citiesTask, suburbTask, propertyTypesTask, registrationTypesTask, lawFirmTask);

            return new PropertyLookupData
            {
                Cities = citiesTask.Result,
                Suburbs = suburbTask.Result,
                RegistrationTypes = registrationTypesTask.Result,
                PropertyTypes = propertyTypesTask.Result,
                LawFirms = lawFirmTask.Result
            };
        }

        public async Task<PaginatedList> GetPropertiesByOwnerId(int ownerId, int pageIndex, int pageSize, string[] searchTerms)
        {
            var properties = await _propertyRepository.GetPropertiesByOwnerId(ownerId, searchTerms);

            return await PaginateResult(properties, pageIndex, pageSize);
        }


        private async Task<PaginatedList> PaginateResult(IEnumerable<Property> items, int pageIndex, int pageSize)
        {
            var itemsList = items.ToList();

            // Handle the case where the list is empty
            if (!itemsList.Any())
            {
                return new PaginatedList
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalPages = 0,
                    TotalCount = 0,
                    Items = new List<Property>()
                };
            }

            // Calculate total pages based on the count of items
            var totalPages = (int)Math.Ceiling(itemsList.Count / (double)pageSize);

            // Ensure pageIndex is within the valid range
            if (pageIndex < 1) pageIndex = 1;
            if (pageIndex > totalPages) pageIndex = totalPages;

            // Create the paginated list
            var pagedList = new PaginatedList
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = itemsList.Count,
                Items = itemsList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
            };

            return pagedList;
        }
    }
}

using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;

namespace Banga.Logic.Services
{
    public class PropertyOfferService : IPropertyOfferService
    {
        private readonly IPropertyOfferRepository _propertyOfferRepository;  
        public PropertyOfferService(IPropertyOfferRepository propertyOfferRepository)
        {
            _propertyOfferRepository = propertyOfferRepository; 
        }

        public Task<long> CreateOffer(PropertyOffer offer)
        {
            return _propertyOfferRepository.CreateOffer(offer);
        }

        public Task DeleteOffer(long offerId)
        {
            return _propertyOfferRepository.DeleteOffer(offerId);
        }

        public Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId)
        {
            return _propertyOfferRepository.GetCurrentBuyerOffer(propertyId, buyerId);
        }

        public Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId)
        {
            return _propertyOfferRepository.GetOffersByPropertyId(propertyId);
        }

        public Task UpdateOffer(PropertyOffer propertyOffer)
        {
            return _propertyOfferRepository.UpdateOffer(propertyOffer); 
        }
    }
}

using Banga.Data.Models;
using Banga.Domain.Enums;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.ViewModels;

namespace Banga.Logic.Services
{
    public class PropertyOfferService : IPropertyOfferService
    {
        private readonly IPropertyOfferRepository _propertyOfferRepository;  
        public PropertyOfferService(IPropertyOfferRepository propertyOfferRepository)
        {
            _propertyOfferRepository = propertyOfferRepository; 
        }

        public async Task ConfirmOffer(long offerId, bool isConfirmed)
        {
            var offer = await _propertyOfferRepository.GetOfferById(offerId);

            if (offer != null)
            {
                offer.StatusId = (int)StatusEnum.Accepted;
                await _propertyOfferRepository.UpdateOffer(offer);
            }
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

        public Task<IEnumerable<VwUserPropertyOffers>> GetPropertyOffersByUserId(long userId)
        {
            return _propertyOfferRepository.GetPropertyOffersByUserId(userId);
        }

        public async Task UpdateOffer(PropertyOffer propertyOffer)
        {
            if(propertyOffer.StatusId == (int)StatusEnum.Accepted)
            {
                var propertyOffers = await _propertyOfferRepository.GetOffersByPropertyId(propertyOffer.PropertyId);

                if(propertyOffers.Count() > 1)
                {
                    foreach (var item in propertyOffers)
                    {
                        if(propertyOffer.StatusId == (int)StatusEnum.Accepted && item.PropertyOfferId != propertyOffer.PropertyOfferId)
                        {
                            item.StatusId = (int)StatusEnum.Created;
                            await _propertyOfferRepository.UpdateOffer(item);
                        }
                    }
                }
            }

            await  _propertyOfferRepository.UpdateOffer(propertyOffer); 
        }
    }
}

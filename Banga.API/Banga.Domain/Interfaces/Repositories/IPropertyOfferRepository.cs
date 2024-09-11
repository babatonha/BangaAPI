
using Banga.Data.Models;
using Banga.Domain.ViewModels;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyOfferRepository
    {
        Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId);
        Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId);
        Task<PropertyOffer> GetOfferById(long offerId);
        Task<long> CreateOffer(PropertyOffer offer);    
        Task DeleteOffer(long offerId);
        Task UpdateOffer(PropertyOffer propertyOffer);
        Task<IEnumerable<VwUserPropertyOffers>> GetPropertyOffersByUserId(long userId);
    }
}

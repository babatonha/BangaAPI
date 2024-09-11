using Banga.Data.Models;
using Banga.Domain.ViewModels;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyOfferService
    {
        Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId);
        Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId);
        Task<long> CreateOffer(PropertyOffer offer);
        Task DeleteOffer(long offerId);
        Task UpdateOffer(PropertyOffer propertyOffer);
        Task<IEnumerable<VwUserPropertyOffers>> GetPropertyOffersByUserId(long userId);
        Task ConfirmOffer(long offerId, bool isConfirmed);
    }
}

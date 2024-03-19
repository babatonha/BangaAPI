using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IPropertyOfferService
    {
        Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId);
        Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId);
        Task<long> CreateOffer(PropertyOffer offer);
        Task DeleteOffer(long offerId);
        Task UpdateOffer(PropertyOffer propertyOffer);
    }
}

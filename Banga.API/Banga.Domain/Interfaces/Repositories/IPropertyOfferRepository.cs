
using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IPropertyOfferRepository
    {
        Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId);
        Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId);
        Task<long> CreateOffer(PropertyOffer offer);    
        Task DeleteOffer(long offerId);
        Task UpdateOffer(PropertyOffer propertyOffer);
    }
}

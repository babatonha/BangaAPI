using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IBuyerListingService
    {
        Task<IEnumerable<BuyerListing>> GetBuyerListing();
        Task<long> CreateBuyerListing(BuyerListing buyerListing);
        Task UpdateBuyerListing(BuyerListing buyerListing);
        Task DeleteBuyerListing(long buyerListingId);
    }
}

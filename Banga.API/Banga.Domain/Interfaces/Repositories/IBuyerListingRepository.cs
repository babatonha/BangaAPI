using Banga.Domain.Models;
using System.Threading.Tasks;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IBuyerListingRepository
    {
        Task<IEnumerable<BuyerListing>> GetBuyerListing();
        Task<long> CreateBuyerListing(BuyerListing buyerListing);
        Task UpdateBuyerListing(BuyerListing buyerListing);
        Task DeleteBuyerListing(long buyerListingId);
    }
}

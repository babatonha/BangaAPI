using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class BuyerListingService: IBuyerListingService
    {
        private readonly IBuyerListingRepository _buyerListingRepository;   
        public BuyerListingService(IBuyerListingRepository buyerListingRepository)
        {
            _buyerListingRepository = buyerListingRepository;
        }

        public Task<long> CreateBuyerListing(BuyerListing buyerListing)
        {
            return _buyerListingRepository.CreateBuyerListing(buyerListing);
        }

        public Task DeleteBuyerListing(long buyerListingId)
        {
            return _buyerListingRepository.DeleteBuyerListing(buyerListingId);  
        }

        public Task<IEnumerable<BuyerListing>> GetBuyerListing()
        {
            return _buyerListingRepository.GetBuyerListing();
        }

        public Task UpdateBuyerListing(BuyerListing buyerListing)
        {
            return _buyerListingRepository.UpdateBuyerListing(buyerListing);
        }
    }
}

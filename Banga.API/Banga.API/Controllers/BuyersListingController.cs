using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersListingController : ControllerBase
    {
        private readonly IBuyerListingService _buyerListingService; 
        public BuyersListingController(IBuyerListingService buyerListingService)
        {
            _buyerListingService = buyerListingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyerListing>>> Get()
        {
            var data = await _buyerListingService.GetBuyerListing();

            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBuyerListing([FromBody] BuyerListing buyer)
        {
            await _buyerListingService.UpdateBuyerListing(buyer);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateBuyerListing([FromBody] BuyerListing buyer)
        {
            var buyerId = await _buyerListingService.CreateBuyerListing(buyer);

            return Ok(buyerId);
        }

        [HttpDelete("buyerListingId")]
        public async Task<ActionResult<int>> DeleteBuyerListing(long buyerListingId)
        {
            await _buyerListingService.DeleteBuyerListing(buyerListingId);

            return Ok();
        }
    }
}

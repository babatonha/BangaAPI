using Banga.Data.Models;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyOfferController : ControllerBase
    {
        private readonly IPropertyOfferService _propertyOfferService;    
        public PropertyOfferController(IPropertyOfferService propertyOfferService)
        {
            _propertyOfferService = propertyOfferService;   
        }

        [HttpGet("{propertyId}/{buyerId}")]
        public async Task<ActionResult<PropertyOffer>> GetCurrentBuyerOffer(long propertyId, int buyerId)
        {
            var offer = await _propertyOfferService.GetCurrentBuyerOffer( propertyId, buyerId);

            return Ok(offer);
        }

        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyOffer>>> GetOffersByPropertyId(long propertyId)
        {
            var offers = await _propertyOfferService.GetOffersByPropertyId(propertyId);

            return Ok(offers);
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateOffer([FromBody] PropertyOffer offer)
        {

            if(offer.PropertyOfferId > 0) 
            {
                await _propertyOfferService.UpdateOffer(offer);

                return Ok(offer.PropertyOfferId);
            }
            var offerId =  await _propertyOfferService.CreateOffer(offer);   

            return Ok(offerId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOffer([FromBody] PropertyOffer offer)
        {
            await _propertyOfferService.UpdateOffer(offer);

            return Ok();
        }

        [HttpDelete("{propertyOfferId}")]
        public async Task<ActionResult> GetPropertyPhotosByPropertyId(long propertyOfferId)
        {
           await _propertyOfferService.DeleteOffer(propertyOfferId);
            return Ok();
        }


    }
}

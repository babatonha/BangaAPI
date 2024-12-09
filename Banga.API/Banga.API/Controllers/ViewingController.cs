using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Banga.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewingController : ControllerBase
    {
        private readonly IViewingService _viewingService;
        public ViewingController(IViewingService viewingService)
        {
            _viewingService = viewingService;
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateViewing([FromBody] Viewing viewing)
        {
            if (viewing.ViewingId > 0)
            {
                await _viewingService.UpdateViewing(viewing);
                return Ok(viewing.ViewingId);
            }
            return Ok(await _viewingService.CreateViewing(viewing));
        }

        [HttpGet("{userId}/{propertyId}")]
        public async Task<ActionResult<IEnumerable<Viewing>>> GetPropertyViewingsByUserId(int userId, long propertyId)
        {
            var data = await _viewingService.GetPropertyViewingsByUserId( userId,  propertyId);

            return Ok(data);
        }

        [HttpDelete("{viewingId}")]
        public async Task<ActionResult<IEnumerable<Viewing>>> Delete(int viewingId)
        {
             await _viewingService.DeleteViewing(viewingId);

            return Ok();
        }
    }
}

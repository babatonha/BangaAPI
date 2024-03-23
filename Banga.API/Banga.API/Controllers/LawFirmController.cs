using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LawFirmController : ControllerBase
    {
        private readonly ILawFirmService _lawFirmService;    
        public LawFirmController(ILawFirmService lawFirmService)
        {
            _lawFirmService = lawFirmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LawFirm>>> Get()
        {
            var data = await _lawFirmService.GetLawFirms();

            return Ok(data);
        }

        [HttpGet("{lawFirmId}")]
        public async Task<ActionResult<IEnumerable<LawFirm>>> Get(int lawFirmId)
        {
            var data = await _lawFirmService.GetLawFirmById(lawFirmId);

            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLawFirm([FromBody] LawFirm firm)
        {
            await _lawFirmService.UpdateLawFirm(firm);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateLawFirm([FromBody] LawFirm firm)
        {
            var firmId = await _lawFirmService.CreateLawFirm(firm);

            return Ok(firmId);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> DisableLawFirm([FromBody] LawFirm firm)
        {
            await _lawFirmService.DisableLawFirm(firm);

            return Ok();
        }

        [HttpGet("Ratings/{lawFirmId}/{userId}")]
        public async Task<ActionResult<LawFirmRating>> GetUserRating(int lawFirmId, int userId)
        {
            var data = await _lawFirmService.GetUserRating(lawFirmId, userId);

            return Ok(data);
        }


        [HttpGet("Ratings/{lawFirmId}")]
        public async Task<ActionResult<IEnumerable<LawFirmRating>>> GetLawFirmRatings(int lawFirmId)
        {
            var data = await _lawFirmService.GetLawFirmRatings(lawFirmId);

            return Ok(data);
        }

        [HttpPost("Ratings")]
        public async Task<ActionResult> CreateRating([FromBody] LawFirmRating rating)
        {
            await _lawFirmService.CreateLawFirmRating(rating);

            return Ok();
        }
    }
}

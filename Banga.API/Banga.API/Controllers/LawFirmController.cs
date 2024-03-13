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
    }
}

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
    }
}

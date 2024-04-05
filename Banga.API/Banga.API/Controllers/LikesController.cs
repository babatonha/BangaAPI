using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;

        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<long>> CreateLike([FromBody] Likes likes)
        { 
            if (likes.LikeId > 0)
            {
                await _likesService.UpdateLike(likes);
                return Ok(likes.LikeId);
            }
            return Ok(await _likesService.CreateLike(likes));
        }

        [HttpGet("{propertyId}/{userId}")]
        public async Task<ActionResult<Likes>> UserHasLiked(long propertyId, int userId)
        {
            return Ok(await _likesService.UserHasLiked(propertyId, userId));
        }
    }
}

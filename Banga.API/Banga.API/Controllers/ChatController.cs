using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("GetChats/{userId}/{toUserId}")]
        public async Task<IActionResult> GetChats(int userId, int toUserId)
        {
            var chats = await _chatService.GetChats(userId, toUserId);

            return Ok(chats);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(SendMessageDto request)
        {
            await _chatService.SendMessage(request);

            return Ok();
        }
    }
}

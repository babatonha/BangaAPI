using AutoMapper;
using Banga.Domain.DTOs;
using Banga.Domain.Helpers;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Banga.Logic.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IUserService userService, IMessageService messageService, IMapper mapper)
        {
            _userService = userService;
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDTO)
        {
            var userName = User.GetUsername();

            if(userName == createMessageDTO.RecipientUsername.ToLower())
            {
                return BadRequest("You can not send messages to yourself");
            }

            var sender = await _userService.GetUserByUserName(userName);
            var recipient = await _userService.GetUserByUserName(createMessageDTO.RecipientUsername);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDTO.Content
            };

            _messageService.AddMessage(message);

            if(await _messageService.SaveAllAsync()) 
            {
                return Ok(_mapper.Map<MessageDTO>(message));
            }

            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _messageService.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage,
                messages.PageSize, messages.TotalCount, messages.TotalPages));

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            var messages = await _messageService.GetMessageThread(currentUsername, username);

            return Ok(messages);
        }

        [HttpDelete("{messageId}")]
        public async Task<ActionResult> DeleteMessage(long messageId)
        {
            var username = User.GetUsername();
            var message = await _messageService.GetMessage(messageId);

            if (message.SenderUsername != username && message.RecipientUsername != username) return Unauthorized();

            if(message.SenderUsername == username) message.SenderDeleted = true;
            if (message.RecipientUsername == username) message.RecipientDeleted = true;

            if(message.SenderDeleted && message.RecipientDeleted)
            {
                _messageService.DeleteMessage(message);
            }
            if (await _messageService.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting the message");    
        }
    }
}

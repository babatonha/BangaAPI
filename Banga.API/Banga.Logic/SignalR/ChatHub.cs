using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Banga.Logic.SignalR
{
    public class ChatHub : Hub
    {

        public readonly IUserService _userService;
        public readonly IChatService _chatService;

        public ChatHub(IUserService userService, IChatService chatService)
        {
            _userService = userService;
            _chatService = chatService;
        }

        public async Task Connect(int userId)
        {

            var existingUserConnection = await _chatService.GetConnectionByUserId(userId);

            if (existingUserConnection == null) 
            {
                await _chatService.AddConnection(Context.ConnectionId, userId);
            }

            else
            {
                await _chatService.UpdateConnection(Context.ConnectionId, userId, existingUserConnection);
            }

            var user = await _userService.GetUserById(userId);
            if (user is not null)
            {
                await _userService.UpdateUserOnlineStatus(userId, true);
                await Clients.All.SendAsync("Users", user);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        { 
            var connection = await _chatService.GetConnectionByConnectionId(Context.ConnectionId);
            await _chatService.RemoveConnection(Context.ConnectionId);
            var user = await _userService.GetUserById(connection.UserId);
            if (user is not null)
            {
                await _userService.UpdateUserOnlineStatus(connection.UserId, false);
                await Clients.All.SendAsync("Users", user);
            }
        }
    }
}

using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Banga.Logic.SignalR
{
    public class ChatHub : Hub
    {

        public readonly IUserService _userService;

        public static Dictionary<string, int> Users = new();

        public ChatHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Connect(int userId)
        {
            Users.Add(Context.ConnectionId, userId);
            var user = await _userService.GetUserById(userId);
            if (user is not null)
            {
                await _userService.UpdateUserOnlineStatus(userId, true);
                await Clients.All.SendAsync("Users", user);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            int userId;
            Users.TryGetValue(Context.ConnectionId, out userId);
            Users.Remove(Context.ConnectionId);
            var user = await _userService.GetUserById(userId);
            if (user is not null)
            {
                await _userService.UpdateUserOnlineStatus(userId, false);
                await Clients.All.SendAsync("Users", user);
            }
        }
    }
}

using Banga.Data;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Banga.Logic.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Banga.Logic.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly DatabaseContext _databaseContext;
        public ChatService(IHubContext<ChatHub> hubContext, DatabaseContext databaseContext)
        {
            _hubContext = hubContext;
            _databaseContext = databaseContext;
        }

        public  async Task<IEnumerable<Chat>> GetChats(int userId, int toUserId)
        {
            return await _databaseContext.Chats .Where(p => p.UserId == userId 
            && p.ToUserId == toUserId 
            || p.ToUserId == userId 
            && p.UserId == toUserId) .OrderBy(p => p.Date).ToListAsync();
        }

        public  async Task SendMessage(SendMessageDto request)
        {
            Chat chat = new()
            {
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.Message,
                Date = DateTime.Now
            };

            await _databaseContext.AddAsync(chat);
            await _databaseContext.SaveChangesAsync();

            string connectionId = ChatHub.Users.First(p => p.Value == chat.ToUserId).Key;

            await _hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);
        }
    }
}

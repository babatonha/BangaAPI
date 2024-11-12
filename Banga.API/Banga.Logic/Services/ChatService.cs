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
            return await _databaseContext.Chats.Where(p => p.UserId == userId 
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

            var connection = await GetConnectionByUserId(request.ToUserId);

            if (connection != null) 
            {
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("Messages", chat);
            }
            else
            {
                connection = await GetConnectionByUserId(request.UserId);
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("Messages", chat);
            }


            await _databaseContext.AddAsync(chat);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Connection> GetConnectionByConnectionId(string connectionId)
        {
            return await _databaseContext.Connections.Where(x => x.ConnectionId == connectionId).FirstOrDefaultAsync();
        }

        public async Task<Connection> GetConnectionByUserId(int userId)
        {
            return await _databaseContext.Connections.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Connection>> GetConnections()
        {
            return await _databaseContext.Connections.ToListAsync(); 
        }

        public async Task RemoveConnection(string connectionId)
        {
            var connection = await GetConnectionByConnectionId(connectionId);
            _databaseContext.Connections.Remove(connection);
        }

        public async Task  AddConnection(string connectionId, int userId)
        {
            var connection = new Connection { ConnectionId = connectionId , UserId = userId};
            await _databaseContext.Connections.AddAsync(connection);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateConnection(string connectionId, int userId, Connection connection)
        {
            _databaseContext.Connections.Remove(connection);
            await AddConnection(connectionId, userId);
        }
    }
}

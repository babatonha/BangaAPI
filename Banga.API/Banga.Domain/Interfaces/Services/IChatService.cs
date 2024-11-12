using Banga.Domain.DTOs;
using Banga.Domain.Models;
using System.Threading.Tasks;

namespace Banga.Domain.Interfaces.Services
{
    public interface IChatService
    {

        Task<IEnumerable<Chat>> GetChats(int userId, int toUserId);
        Task SendMessage(SendMessageDto messageDto);
        Task AddConnection(string connectionId, int userId);
        Task RemoveConnection(string connectionId);
        Task<IEnumerable<Connection>> GetConnections();
        Task<Connection> GetConnectionByConnectionId(string connectionId);
        Task<Connection> GetConnectionByUserId(int userId);
        Task UpdateConnection(string connectionId, int userId, Connection connection);
    }
}

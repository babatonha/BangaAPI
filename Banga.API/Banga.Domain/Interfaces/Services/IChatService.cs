using Banga.Domain.DTOs;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IChatService
    {

        Task<IEnumerable<Chat>> GetChats(int userId, int toUserId);
        Task SendMessage(SendMessageDto messageDto);   
    }
}

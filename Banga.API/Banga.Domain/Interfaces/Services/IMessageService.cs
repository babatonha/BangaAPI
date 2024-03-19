using Banga.Domain.DTOs;
using Banga.Domain.Helpers;
using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(long id);
        Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUserName, string recipientUserName);
        Task<bool> SaveAllAsync();
    }
}

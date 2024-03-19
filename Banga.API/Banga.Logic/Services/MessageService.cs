using AutoMapper;
using AutoMapper.QueryableExtensions;
using Banga.Data;
using Banga.Domain.DTOs;
using Banga.Domain.Helpers;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly  DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public MessageService(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }
        public void AddMessage(Message message)
        {
            _databaseContext.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _databaseContext.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(long id)
        {
            return await _databaseContext.Messages.FindAsync(id);
        }

        public  async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _databaseContext.Messages
                            .OrderByDescending(x => x.MessageSent)
                            .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username &&
                           u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username &&
                           u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.UserName == messageParams.Username
                    && u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDTO>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var query = _databaseContext.Messages
                 .Where(
                     m => m.RecipientUsername == currentUserName && m.RecipientDeleted == false &&
                     m.SenderUsername == recipientUserName ||
                     m.RecipientUsername == recipientUserName && m.SenderDeleted == false &&
                     m.SenderUsername == currentUserName
                 )
                 .OrderBy(m => m.MessageSent)
                 .AsQueryable();

            var unreadMessages = query.Where(m => m.DateRead == null
                && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
            }

            await SaveAllAsync();

            return await query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _databaseContext.SaveChangesAsync() > 0;
        }
    }
}

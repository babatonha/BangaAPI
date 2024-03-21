using AutoMapper;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Banga.Logic.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Banga.Logic.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHubContext<PresenceHub> _presenceHub;

        public MessageHub(IMessageService messageService, IUserService userService, IMapper mapper, IHubContext<PresenceHub> presenceHub)
        {
            _messageService = messageService;
            _userService = userService;
            _mapper = mapper;
            _presenceHub = presenceHub;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"];
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _messageService.GetMessageThread(Context.User.GetUsername(), otherUser);

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDTO createMessageDTO)
        {

            var userName = Context.User.GetUsername();

            if (userName == createMessageDTO.RecipientUsername.ToLower())
            {
                throw new HubException("You can not send messages to yourself");
            }

            var sender = await _userService.GetUserByUserName(userName);
            var recipient = await _userService.GetUserByUserName(createMessageDTO.RecipientUsername);

            if (recipient == null) throw new HubException("Not found");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDTO.Content
            };

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await _messageService.GetMessageGroup(groupName);


            if (group.Connections.Any(x => x.Username == recipient.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                var connections = await PresenceTracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.UserName, knownAs = sender.FirstName });
                }
            }
        

            _messageService.AddMessage(message);

            if (await _messageService.SaveAllAsync())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDTO>(message));
            }
        }

        private string GetGroupName(string caller, string other)//alphabetical order
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;

            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _messageService.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new Group(groupName);
                _messageService.AddGroup(group);
            }

            group.Connections.Add(connection);

            if (await _messageService.SaveAllAsync()) return group;

            throw new HubException("Failed to add to group");
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _messageService.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections
                .FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            _messageService.RemoveConnection(connection);

            if (await _messageService.SaveAllAsync()) return group;

            throw new HubException("Failed to remove from group");
        }
    }
}

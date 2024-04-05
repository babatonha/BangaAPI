using Banga.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Banga.Data.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? IdNumber { get; set; }
        public bool? IsBlocked { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public List<Message> MessagesSend { get; set; }
        public List<Message> MessagesReceived { get; set; }
    }
}

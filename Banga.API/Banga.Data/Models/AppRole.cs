using Microsoft.AspNetCore.Identity;

namespace Banga.Data.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}

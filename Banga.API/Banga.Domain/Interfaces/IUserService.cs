using Banga.Data.Models;

namespace Banga.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(int userId); 
    }
}

using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(int userId);

        Task<AppUser> GetUserByUserName(string username);
    }
}

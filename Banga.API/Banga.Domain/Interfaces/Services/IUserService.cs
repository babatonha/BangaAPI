using Banga.Data.Models;
using Banga.Domain.DTOs;

namespace Banga.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(int userId);
        Task<AppUser> GetUserByUserName(string username);
        Task UpdateUser(CreateUserDTO user);
        Task AssignUserRole(int userId, string roleName);
        Task BlockUser(int userId, bool isBlocked);
    }
}

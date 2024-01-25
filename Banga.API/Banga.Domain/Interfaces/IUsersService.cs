using Banga.Data.Models;

namespace Banga.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int userId); 
    }
}

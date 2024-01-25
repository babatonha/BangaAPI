using Banga.Data;
using Banga.Data.Models;
using Banga.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class UsersService : IUsersService
    {
        private readonly DatabaseContext _databaseContext;
        public UsersService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _databaseContext.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _databaseContext.Users.ToListAsync();
        }
    }
}

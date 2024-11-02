using Banga.Data;
using Banga.Data.Models;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        public UserService(UserManager<AppUser> userManager, DatabaseContext databaseContext)
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        public async Task AssignUserRole(int userId, string roleName)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();   
            var currentRole = await _userManager.GetRolesAsync(user);   

            if(currentRole.Any()) // this system will always assign one role to a user.
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole[0]);
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task BlockUser(int userId, bool isBlocked)
        {
            var existingUser = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                existingUser.IsBlocked = isBlocked;
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<AppUser> GetUserById(int userId)
        {
            return await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByUserName(string username)
        {
            return await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task UpdateUser(CreateUserDTO user)
        {
            var existingUser = await _userManager.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if(existingUser != null)
            {
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.IdNumber = user.IdNumber;
                existingUser.Email = user.Email;

                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserOnlineStatus(int userId, bool isOnline)
        {
            var existingUser = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                existingUser.IsOnline = isOnline;
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}

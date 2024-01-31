using Banga.Data.Models;
using Banga.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;   
        }

        public async Task<AppUser> GetUserById(int userId)
        {
            return await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async  Task<AppUser> Login(AppUser userData)
        {
            return  new AppUser
            {
                Id = 1,
                UserName = _tokenService.CreateToken(userData)
            };
        }

        public Task<AppUser> Register(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}

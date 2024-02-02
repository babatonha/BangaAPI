using Banga.Data.Models;
using Banga.Domain;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces;
using Banga.Domain.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async  Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                                    //.Include(p => p.Photos)
                                    .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return new UserDto();
            } 

            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = UserMapper.MappRegisterDtoToAppUser(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);


            //var roleResult = await _userManager.AddToRoleAsync(user, UserType.Member.ToString());

            return new UserDto
            {
                UserName = user.UserName,
                Email = registerDto.Email,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}

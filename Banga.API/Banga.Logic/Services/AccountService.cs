using Banga.Data.Models;
using Banga.Domain.DTOs;
using Banga.Domain.Enums;
using Banga.Domain.Helpers;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;   
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
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = UserMapper.MappRegisterDtoToAppUser(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, UserType.User.ToString());

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = registerDto.Email,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword,newPassword);
        }

        public async Task<IdentityResult> ForgotPassword(AppUser user)
        {
            var newPassword = PasswordHelper.GenerateRandomPassword();
            var result = new IdentityResult();

            if (!String.IsNullOrWhiteSpace(newPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            }

            if (user.Email != null && result.Succeeded)
            {
                _emailService.SendEmail(
                        user.Email,
                        "New Password",
                        $"Your temporary password is: {newPassword}. Please change it immediately."
                );

            }
 
            return result;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        public async Task<AppUser> GetCurrentUserByUsername(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower() || x.Email == username.ToLower());
        }
    }
}

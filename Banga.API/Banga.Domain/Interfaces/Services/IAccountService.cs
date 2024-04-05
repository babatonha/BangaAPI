using Banga.Data.Models;
using Banga.Domain.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Banga.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto);
        Task<bool> UserExists(string username);
        Task<IdentityResult> ForgotPassword(AppUser user);
        Task<IdentityResult> ChangePassword(AppUser user, string oldPassword, string newPassword);
        Task<AppUser> GetCurrentUserByUsername(string username);
    }
}

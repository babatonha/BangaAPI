using Banga.Data.Models;
using Banga.Domain.DTOs;

namespace Banga.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto);
        Task<bool> UserExists(string username);
    }
}

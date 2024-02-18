using Banga.Data.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}

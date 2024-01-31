using Banga.Data.Models;

namespace Banga.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);    
    }
}

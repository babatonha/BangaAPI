using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IViewingService
    {
        Task<IEnumerable<Viewing>> GetViewingsByUserId(int userId);
        Task<long> CreateViewing(Viewing viewing);
        Task UpdateViewing(Viewing viewing);
        Task DeleteViewing(long viewing);
    }
}

using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface IViewingService
    {
        Task<IEnumerable<Viewing>> GetPropertyViewingsByUserId(int userId, long propertyId);
        Task<long> CreateViewing(Viewing viewing);
        Task UpdateViewing(Viewing viewing);
        Task DeleteViewing(long viewing);
    }
}

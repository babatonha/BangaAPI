using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface IViewingRepository
    {
        Task<IEnumerable<Viewing>> GetPropertyViewingsByUserId(int userId, long propertyId);
        Task<long> CreateViewing(Viewing viewing);
        Task UpdateViewing(Viewing viewing);
        Task DeleteViewing(long viewingId);
    }
}

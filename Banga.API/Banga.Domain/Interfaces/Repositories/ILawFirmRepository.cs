using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface ILawFirmRepository
    {
        Task<IEnumerable<LawFirm>> GetLawFirms();
        Task<LawFirm> GetLawFirmById(int lawFirmId);
        Task<int> CreateLawFirm(LawFirm firm);
        Task UpdateLawFirm(LawFirm firm);
        Task DisableLawFirm(LawFirm firm);

        Task<long> CreateLawFirmRating(LawFirmRating rating);
        Task<LawFirmRating> GetUserRating(int lawFirmId, int userId);
        Task UpdateLawFirmTotalRating(int lawFirmId, double ratingTotal);
        Task<IEnumerable<LawFirmRating>> GetLawFirmRatings(int lawFirmId);
    }
}

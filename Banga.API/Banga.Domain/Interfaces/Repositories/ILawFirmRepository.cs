using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface ILawFirmRepository
    {
        Task<IEnumerable<LawFirm>> GetLawFirms();
        Task<int> CreateLawFirm(LawFirm firm);
        Task UpdateLawFirm(LawFirm firm);
        Task DisableLawFirm(LawFirm firm);
    }
}

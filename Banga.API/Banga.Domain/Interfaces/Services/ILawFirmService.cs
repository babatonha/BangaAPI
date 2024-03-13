using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface ILawFirmService
    {
        Task<IEnumerable<LawFirm>> GetLawFirms();
        Task<int> CreateLawFirm(LawFirm firm);
        Task UpdateLawFirm(LawFirm firm);
        Task DisableLawFirm(LawFirm firm);
    }
}

using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface ILawFirmService
    {
        Task<IEnumerable<LawFirm>> GetLawFirms();
    }
}

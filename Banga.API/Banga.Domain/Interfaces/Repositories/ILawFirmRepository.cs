using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface ILawFirmRepository
    {
        Task<IEnumerable<LawFirm>> GetLawFirms();    
    }
}

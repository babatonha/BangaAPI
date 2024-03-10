using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class LawFirmService: ILawFirmService
    {
        private readonly ILawFirmRepository _lawFirmRepository;
        public LawFirmService(ILawFirmRepository lawFirmRepository)
        {
            _lawFirmRepository = lawFirmRepository; 
        }

        Task<IEnumerable<LawFirm>> ILawFirmService.GetLawFirms()
        {
            return _lawFirmRepository.GetLawFirms();
        }
    }
}

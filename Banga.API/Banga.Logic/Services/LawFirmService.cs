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

        public Task<int> CreateLawFirm(LawFirm firm)
        {
            return _lawFirmRepository.CreateLawFirm(firm);
        }

        public async Task<long> CreateLawFirmRating(LawFirmRating rating)
        {
            var lawFirmRatings = await _lawFirmRepository.GetLawFirmById(rating.LawFirmId);
            var totalRating = rating.Rating;

            if (lawFirmRatings != null)
            {
                var currentRating = lawFirmRatings.TotalRating > 0 ? lawFirmRatings.TotalRating : 0.0;
                totalRating = (currentRating + totalRating) / 2;
            }

            await _lawFirmRepository.UpdateLawFirmTotalRating(rating.LawFirmId, totalRating);

           return  await _lawFirmRepository.CreateLawFirmRating(rating); 
        }

        public Task DisableLawFirm(LawFirm firm)
        {
            return _lawFirmRepository.DisableLawFirm(firm);
        }

        public Task<IEnumerable<LawFirmRating>> GetLawFirmRatings(int lawFirmId)
        {
            return _lawFirmRepository.GetLawFirmRatings(lawFirmId);
        }

        public Task<LawFirmRating> GetUserRating(int lawFirmId, int userId)
        {
            return _lawFirmRepository.GetUserRating(lawFirmId, userId);
        }

        public Task UpdateLawFirm(LawFirm firm)
        {
            return _lawFirmRepository.UpdateLawFirm(firm);
        }

        public Task UpdateLawFirmTotalRating(int lawFirmId, double ratingTotal)
        {
            return _lawFirmRepository.UpdateLawFirmTotalRating(lawFirmId, ratingTotal); 
        }

        public Task<IEnumerable<LawFirm>> GetLawFirms()
        {
            return _lawFirmRepository.GetLawFirms();
        }

        public Task<LawFirm> GetLawFirmById(int firmId)
        {
            return _lawFirmRepository.GetLawFirmById(firmId);
        }
    }
}

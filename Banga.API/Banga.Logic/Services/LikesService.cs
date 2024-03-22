using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IPropertyService _propertyService;
        public LikesService(ILikesRepository likesRepository, IPropertyService propertyService)
        {
            _likesRepository = likesRepository;
            _propertyService = propertyService; 
        }

        public async Task<long> CreateLike(Likes likes)
        {
            long likedId = 0;
            var property = await _propertyService.GetPropertyDetailsById(likes.PropertyId);

            var numberOfLikes = likes.IsLiked ? property.Property.NumberOfLikes + 1 : property.Property.NumberOfLikes - 1;

            if (property != null && numberOfLikes != null)
            {
                var likesTask = _likesRepository.CreateLike(likes);
                var propertyTask =  UpdatePropertyLikes(likes.PropertyId, (int)numberOfLikes);

                await  Task.WhenAll(likesTask, propertyTask);
                likedId = likesTask.Result; 
            }
            return likedId;
        }

        public async Task UpdateLike(Likes likes)
        {
            var property = await _propertyService.GetPropertyDetailsById(likes.PropertyId);

            var numberOfLikes = likes.IsLiked ? property.Property.NumberOfLikes + 1 : property.Property.NumberOfLikes - 1;

            if (property != null && numberOfLikes != null)
            {
                var likesTask = _likesRepository.UpdateLike(likes);
                var propertyTask = UpdatePropertyLikes(likes.PropertyId, (int)numberOfLikes);

                await Task.WhenAll(likesTask, propertyTask);
            }
        }

        public async Task UpdatePropertyLikes(long propertyId, int numberOfLikes)
        {
            await _likesRepository.UpdatePropertyLikes(propertyId, numberOfLikes);
        }

        public Task<Likes> UserHasLiked(long propertyId, int userId)
        {
            return _likesRepository.UserHasLiked(propertyId, userId);
        }
    }
}

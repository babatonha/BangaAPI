using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Repositories
{
    public interface ILikesRepository
    {
        Task<long> CreateLike(Likes likes);
        Task UpdateLike(Likes likes);
        Task<Likes> UserHasLiked(long propertyId, int  userId);  
        Task UpdatePropertyLikes(long propertyId, int numberOfLikes);
    }
}

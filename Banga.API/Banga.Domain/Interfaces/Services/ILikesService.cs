using Banga.Domain.Models;

namespace Banga.Domain.Interfaces.Services
{
    public interface ILikesService
    {
        Task<long> CreateLike(Likes likes);
        Task UpdateLike(Likes likes);
        Task<Likes> UserHasLiked(long propertyId, int userId);
        Task UpdatePropertyLikes(long propertyId, int numberOfLikes);
    }
}

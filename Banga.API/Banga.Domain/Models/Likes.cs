namespace Banga.Domain.Models
{
    public class Likes
    {
       public long LikeId { get; set; }
       public long PropertyId { get; set; }
       public int UserId { get; set; }
       public bool IsLiked { get; set; }
    }
}

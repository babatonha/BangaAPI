namespace Banga.Domain.Models
{
    public class LawFirmRating
    {
        public long RatingId { get; set; }
        public int LawFirmId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string? Review { get; set; }
        public string? UserName { get; set; }
        public string?  UserFullName { get; set; }
    }
}

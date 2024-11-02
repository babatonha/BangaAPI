namespace Banga.Domain.Models
{
    public class Chat
    {
        public Chat()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}

namespace Banga.Domain.Interfaces.Services
{
    public interface IMailjetService
    {
        Task SendEmail(string recipientEmail, string recipientName, string message);
    }
}

namespace Banga.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}

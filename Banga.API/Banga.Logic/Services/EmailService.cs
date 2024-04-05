using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Banga.Logic.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _from;
        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSsl;

        public EmailService(IOptions<EmailSettings> config)
        {
            _from = config.Value.FromAddress;
            _username = config.Value.Username;
            _password = config.Value.Password;
            _host = config.Value.Host;
            _port = config.Value.Port;
            _useSsl = config.Value.UseSSL;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            client.Connect(_host,_port, _useSsl);
            client.Authenticate(_username, _password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}

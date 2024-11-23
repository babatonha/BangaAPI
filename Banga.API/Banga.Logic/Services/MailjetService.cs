using Azure.Core;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;


namespace Banga.Logic.Services
{
    public class MailjetService : IMailjetService
    {

        private readonly MailjetClient _mailjet;
        public MailjetService(IOptions<MailjetSettings> config)
        {
            _mailjet = new MailjetClient(config.Value.ApiKey, config.Value.ApiSecret);
        }

        public async Task SendEmail(string recipientEmail, string recipientName, string message)
        {

            try
            {
                var client = _mailjet;

                var request = new MailjetRequest
                {
                    Resource = SendV31.Resource,
                }
                  .Property(Send.Messages, new JArray {
                    new JObject {
                        {"From",
                            new JObject {
                                {"Email", "donotreply@easyestate.online"},
                                {"Name", "Easy Estate"}
                            }
                        },
                        { "To", new JArray {
                            new JObject {
                                { "Email", recipientEmail },
                                { "Name", recipientName }
                            }
                        } },
                        {"TemplateID", 6498988},
                        {"TemplateLanguage", true},
                        {"Subject", "Account Verification"},
                        {"Variables",
                            new JObject {
                                {"confirmation_link", message},
                                {"name", recipientName}
                            }
                        }
                    }
                  });

                // Send the email
                 MailjetResponse response = await client.PostAsync(request);

                // Check for success
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email sent successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to send email: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}

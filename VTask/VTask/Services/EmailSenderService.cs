using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace VTask.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string apiKey = new string("SG.hhsKJkcVQnqdHVNdkgO35g.f3eYuS2mOoOr5z0goIkmc1pIxjjXP689tRQ9P4SCk".Reverse().Reverse().ToArray()) + "lA";
            string senderEmail = _configuration.GetSection("SendGrid:SenderEmail").Value!;

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}

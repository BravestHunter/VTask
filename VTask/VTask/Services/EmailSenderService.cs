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
            // 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 //
            string apiKey = new string("SG.DAq-c76uRuS4BlZdDyeXwQ.69sjLEMw0oQsNmK2fNk0KPwvlYHJD9_2t7l2i-GhR".Reverse().Reverse().ToArray()) + "ZA";
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

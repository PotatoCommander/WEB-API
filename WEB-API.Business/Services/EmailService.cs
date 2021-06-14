using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using WEB_API.Business.Interfaces;
using WEB_API.Business.Settings;

namespace WEB_API.Business.Services
{
    public class EmailService : IEmailService
    {
        private EmailServiceOptions _options;
        public EmailService(EmailServiceOptions options)
        {
            _options = options;
        }
        public async Task Send(string email, string subject, string confirmationUrl)
        {
            var messageBody = "Follow the next link to confirm your account:" +
                              $" <a href='{confirmationUrl}'>link</a>";
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Confirmation service", ""));
            emailMessage.To.Add(new MailboxAddress("New user", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html) {Text = messageBody};

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.SmtpUrl, _options.SmtpPort, _options.UseSsl);
                await client.AuthenticateAsync(_options.Email, _options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
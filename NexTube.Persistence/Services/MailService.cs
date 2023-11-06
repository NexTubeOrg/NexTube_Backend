using NexTube.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;

namespace NexTube.Persistence.Services {
    public class MailService : IMailService {

        private readonly IConfiguration? _configuration;

        public MailService(IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string message, string recipient) {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("SMTP:Username")));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = "Nextube Support";
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_configuration.GetValue<string>("SMTP:Server"), _configuration.GetValue<int>("SMTP:Port"), true);
            smtp.Authenticate(_configuration.GetValue<string>("SMTP:Username"), _configuration.GetValue<string>("SMTP:Pwd"));
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string GeneratePassword(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

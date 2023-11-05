using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexTube.Application.Common.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;
using System.Net.Mail;

namespace NexTube.Persistence.Services
{
    public class MailService : IMailService
    {
      
        private readonly IConfiguration? _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

            async public Task SendMail(string message,string recipient) { 
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

        public string GeneratePassword(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}

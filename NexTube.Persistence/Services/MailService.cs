using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexTube.Application.Common.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace NexTube.Persistence.Services
{
    public class MailService : IMailService
    {
        SmtpClient smtpClient;
        private readonly IConfiguration _configuration;
        public MailService (IConfiguration configuration)
        {  
            _configuration = configuration;
            this.smtpClient = new SmtpClient(_configuration.GetValue<string>("SMTP:Server"))
            {
                Port = _configuration.GetValue<int>("SMTP:Port"),
                Credentials = new NetworkCredential(_configuration.GetValue<string>("SMTP:Username"), _configuration.GetValue<string>("SMTP:Pwd")),
                EnableSsl = true
            }; 
        }
       async public Task SendMail(string message,string recipient) { 
         var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration.GetValue<string>("SMTP:Username")),
                Subject = "Nextube Support",
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recipient);
            smtpClient.Send(mailMessage);
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

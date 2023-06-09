﻿using FastFood_Web.Service.Interfaces.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace FastFood_Web.Service.Services.Common.EmailServic
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("EmailSettings");
        }

        public async Task<bool> SendAsync(ViewModel.Helper.EmailMessage emailMessage)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration["Email"]));
            mail.To.Add(MailboxAddress.Parse(emailMessage.To));
            mail.Subject = emailMessage.Subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

            var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Host"], int.Parse(_configuration["Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Email"], _configuration["Password"]);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
            return true;
        }
    }
}



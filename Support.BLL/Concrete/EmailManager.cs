using Microsoft.Extensions.Options;
using Support.BLL.Abstract;
using Support.Entity.DTO.EmailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly EmailSettingsDto _emailSettings;

        public EmailManager(IOptions<EmailSettingsDto> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mericasd2051@gmail.com", "hFg4SUyXLnwZ10Nv"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("mericasd2051@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SendPasswordByEmail(string userEmail, string newPassword)
        {
            string subject = "Support Panel Hoşgeldiniz";
            string body = $"Yeni şifreniz: {newPassword}";

            SendEmail(userEmail, subject, body);
        }
    }
}

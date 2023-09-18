using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Support.Entity.DTO.EmailDtos;

namespace Support.BLL.Abstract
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string subject, string body);
        void SendPasswordByEmail(string userEmail, string newPassword);
    }

}

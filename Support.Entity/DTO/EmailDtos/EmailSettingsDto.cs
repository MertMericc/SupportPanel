using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.EmailDtos
{
    public class EmailSettingsDto
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; } 
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}

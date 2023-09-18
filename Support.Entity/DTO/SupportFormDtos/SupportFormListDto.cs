using Support.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.SupportFormDtos
{
    public class SupportFormListDto:IDto
    {
        public int FormId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

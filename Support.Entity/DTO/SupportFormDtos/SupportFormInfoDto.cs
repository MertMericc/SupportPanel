using Support.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.SupportFormDtos
{
    public class SupportFormInfoDto:IDto
    {
        public int FormId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}

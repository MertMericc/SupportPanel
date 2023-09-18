using Support.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.SupportFormDtos
{
    public class SupportFormUpdateDto:IDto
    {
        public int SupportFormId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}

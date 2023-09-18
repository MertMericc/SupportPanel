using Support.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.Concrete
{
    public class SupportForm:IEntity
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}

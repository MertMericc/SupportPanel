using Support.Core.Entity;
using Support.Entity.DTO.SupportFormDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.UserDtos
{
    public class UserListDto:IDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<SupportFormInfoDto> ActiveForms { get; set; }
        public List<SupportFormInfoDto> WaitingForms { get; set; }
        public List<SupportFormInfoDto> CancelledForms { get; set; }
    }
}

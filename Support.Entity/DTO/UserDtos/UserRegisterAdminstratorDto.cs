using Support.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Entity.DTO.UserDtos
{
    public class UserRegisterAdminstratorDto : IDto
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}

using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Core.Security;
using Support.Entity.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Abstract
{
    public interface IAuthService
    {
        IDataResult<string> Login(UserLoginDto userLoginDto);
        IDataResult<User> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<bool> RegisterForAdmin(UserRegisterAdminstratorDto userRegisterAdminstratorDto);



    }
}

using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Entity.Concrete;
using Support.Entity.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<UserListDto>> GetList();
        IDataResult<User> GetById(int userId);
        IDataResult<bool> UpdateUser(UserUpdateDto userUpdateDto);
        IDataResult<bool> ChangeStatus(int userId);
        IDataResult<User> Get(Expression<Func<User, bool>> filter);

    }
}

using Support.BLL.Abstract;
using Support.BLL.Constants;
using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Core.Security;
using Support.Dal.Abstract;
using Support.Entity.Concrete;
using Support.Entity.DTO.SupportFormDtos;
using Support.Entity.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Concrete
{
    public class UserManager : IUserService
    {

        private readonly IUserDal _userDal;
        private readonly ISupportFormService _supportFormService;

        public UserManager(IUserDal userDal ,ISupportFormService supportFormService)
        {
            _userDal = userDal;
            _supportFormService = supportFormService;
        }

        public IDataResult<bool> ChangeStatus(int userId)
        {
            try
            {
                var getUser = _userDal.Get(x => x.Id == userId);
                if (getUser == null)
                {
                    return new ErrorDataResult<bool>(false,"user not found",Messages.err_null);
                }
                getUser.Status=!getUser.Status;
                _userDal.Update(getUser);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<User> Get(Expression<Func<User, bool>> filter)
        {
            try
            {
                var getUser = _userDal.Get(filter);
                if (getUser==null)
                {
                    return new ErrorDataResult<User>(null, "user not found", Messages.err_null);
                }
                return new SuccessDataResult<User>(getUser);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<User> GetById(int userId)
        {
            try
            {
                var user = _userDal.Get(x => x.Id == userId);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<User> GetByMail(string email)
        {
            try
            {
                var user = _userDal.Get(x => x.Email == email);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }


        public IDataResult<List<UserListDto>> GetList()
        {
            try
            {
                var getUserList = _userDal.GetList().ToList();
                if (getUserList.Count == 0)
                {
                    return new ErrorDataResult<List<UserListDto>>(new List<UserListDto>(), "users not found", Messages.err_null);
                }
                var userList = new List<UserListDto>();

                foreach (var user in getUserList)
                {
                    var activeForms = _supportFormService.GetActiveSupportList(user.Id).Data ?? new List<SupportFormListDto>();
                    var waitingForms = _supportFormService.GetWaitingSupportList(user.Id).Data ?? new List<SupportFormListDto>();
                    var cancelledForms = _supportFormService.GetCancelledSupportList(user.Id).Data ?? new List<SupportFormListDto>();

                    userList.Add(new UserListDto
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        ActiveForms = activeForms.Select(form => new SupportFormInfoDto
                        {
                            FormId = form.FormId,
                            Subject = form.Subject,
                            Message = form.Message
                        }).ToList(),
                        WaitingForms = waitingForms.Select(form => new SupportFormInfoDto
                        {
                            FormId = form.FormId,
                            Subject = form.Subject,
                            Message = form.Message
                        }).ToList(),
                        CancelledForms = cancelledForms.Select(form => new SupportFormInfoDto
                        {
                            FormId = form.FormId,
                            Subject = form.Subject,
                            Message = form.Message
                        }).ToList(),
                    });
                }

                return new SuccessDataResult<List<UserListDto>>(userList, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserListDto>>(new List<UserListDto>(), ex.Message, Messages.err_null);
            }
        }


        public IDataResult<bool> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(userUpdateDto.Token) as JwtSecurityToken;
                var userId = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var getUser = _userDal.Get(x => x.Id == userId);

                if (!HashingHelper.VerifyPasswordHash(userUpdateDto.CurrentPassword, getUser.PasswordSalt, getUser.PasswordHash))
                {
                    return new ErrorDataResult<bool>(false, "Current password is wrong!", Messages.err_null);
                }

                if (!string.IsNullOrEmpty(userUpdateDto.Name))
                {
                    getUser.Name = userUpdateDto.Name;
                }

                if (!string.IsNullOrEmpty(userUpdateDto.Surname))
                {
                    getUser.Surname = userUpdateDto.Surname;
                }

                if (!string.IsNullOrEmpty(userUpdateDto.Email))
                {
                    getUser.Email = userUpdateDto.Email;
                }

                if (!string.IsNullOrEmpty(userUpdateDto.NewPassword))
                {
                    byte[] passwordsalt, passwordhash;
                    HashingHelper.CreatePasswordHash(userUpdateDto.NewPassword, out passwordsalt, out passwordhash);

                    getUser.PasswordSalt = passwordsalt;
                    getUser.PasswordHash = passwordhash;
                }

                _userDal.Update(getUser);
                return new SuccessDataResult<bool>(true, "User updated successfully", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }


       
    }
}

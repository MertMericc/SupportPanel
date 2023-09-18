using Support.BLL.Abstract;
using Support.BLL.Constants;
using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Core.Security;
using Support.Dal.Abstract;
using Support.Entity.Concrete;
using Support.Entity.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper;
        private readonly IUserDal _userDal;
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IEmailService _emailService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal, IUserOperationClaimDal userOperationClaimDal ,IEmailService emailService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _userOperationClaimDal = userOperationClaimDal;
            _emailService = emailService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            try
            {
                var claims = _userService.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, claims.Data);
                return new SuccessDataResult<AccessToken>(accessToken, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<AccessToken>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<string> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var userToCheck = _userService.GetByMail(userLoginDto.Email);
                if (userToCheck.Data == null)
                {
                    return new ErrorDataResult<string>(null, "user not found", Messages.err_null);
                }
                if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.Data.PasswordSalt, userToCheck.Data.PasswordHash))
                {
                    return new ErrorDataResult<string>(null, "invalid password", Messages.err_null);
                }

                var tokenResult = CreateAccessToken(userToCheck.Data);
                if (tokenResult.Success)
                {
                    var accessToken = tokenResult.Data;
                    return new SuccessDataResult<string>(accessToken.Token, "user login successful", Messages.success);

                }
                else
                {
                    return new ErrorDataResult<string>(null, "Token creation error", Messages.err_null);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(null, ex.Message, Messages.err_null);
            }

        }

        public IDataResult<bool> RegisterForAdmin(UserRegisterAdminstratorDto userRegisterAdminstratorDto)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(userRegisterAdminstratorDto.Token) as JwtSecurityToken;
                var userId = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var userDb = _userDal.Get(x => x.Id == userId);
                var userClaims = _userOperationClaimDal.Get(x => x.UserId == userId);

                if (userClaims.OperationClaimId != 2)
                {
                    return new ErrorDataResult<bool>(false, "you are not authorized", Messages.err_null);
                }
                var checkUser = _userService.GetByMail(userRegisterAdminstratorDto.Email);
                if (checkUser != null)
                {
                    return new ErrorDataResult<bool>(false, "user already exist", Messages.err_null);
                }
                byte[] passwordsalt, passwordhash;
                var createdPasswordBySystem = RandomString(8);
                HashingHelper.CreatePasswordHash(createdPasswordBySystem, out passwordsalt, out passwordhash);
                var user = new User
                {
                    Name = userRegisterAdminstratorDto.Name,
                    Surname = userRegisterAdminstratorDto.Surname,
                    Email = userRegisterAdminstratorDto.Email,
                    PasswordHash = passwordhash,
                    PasswordSalt = passwordsalt,
                    Status = true
                };
                _userDal.Add(user);

                var userOperationClaim = new UserOperationClaim()
                {
                    UserId=user.Id,
                    OperationClaimId=1
                };
                _userOperationClaimDal.Add(userOperationClaim);
                _emailService.SendEmail(userRegisterAdminstratorDto.Email, "SupportPanel Hoşgeldiniz", $"Yeni şifreniz: {createdPasswordBySystem}");
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false,ex.Message,Messages.err_null);
            }
           
        }

        public IDataResult<User> UserExists(string email)
        {
            try
            {
                var result = _userDal.Get(x=>x.Email==email);
                if (result != null)
                {
                    return new ErrorDataResult<User>(null, "this user is registered in the system", Messages.err_null);
                }
                return new SuccessDataResult<User>(result, "Ok", Messages.success);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

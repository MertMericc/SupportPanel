using Support.BLL.Abstract;
using Support.BLL.Constants;
using Support.Core.Entity.Concrete;
using Support.Core.Result;
using Support.Dal.Abstract;
using Support.Dal.Concrete.EntityFramework;
using Support.Entity.Concrete;
using Support.Entity.DTO.SupportFormDtos;
using Support.Entity.DTO.UserDtos;
using Support.Entity.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Support.BLL.Concrete
{
    public class SupportFormManager : ISupportFormService
    {
        private readonly ISupportFormDal _formDal;
        private readonly IUserDal _userDal;

        public SupportFormManager(ISupportFormDal formDal, IUserDal userDal)
        {
            _formDal = formDal;
            _userDal = userDal;
        }

        public IDataResult<bool> Add(SupportFormAddDto supportFormAddDto)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(supportFormAddDto.Token) as JwtSecurityToken;
                var userId = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var getUser = _userDal.Get(x=>x.Id==userId);
                SupportForm supportForm = new()
                {
                    Subject = supportFormAddDto.Subject,
                    Message = supportFormAddDto.Message,
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    Status = (int)SupportFormStatusEnum.Active
                };
                _formDal.Add(supportForm);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {
                 return new SuccessDataResult<bool>(false,ex.Message,Messages.err_null);
            }
        }

        public IDataResult<bool> ChangeStatus(int formId,int statusId)
        {
            try
            {
                var getForm = _formDal.Get(x => x.Id == formId);
                if (getForm==null)
                {
                    return new ErrorDataResult<bool>(false, "form not found", Messages.err_null);
                }
                getForm.Status = statusId;
                _formDal.Update(getForm);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<SupportForm> Get(Expression<Func<SupportForm, bool>> filter)
        {
            try
            {
                var getForm = _formDal.Get(filter);
                if (getForm==null)
                {
                    return new ErrorDataResult<SupportForm>(null, "form not found", Messages.err_null);
                }
                return new SuccessDataResult<SupportForm>(getForm, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SupportForm>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<SupportFormListDto>> GetActiveSupportList(int? userId)
        {
            try
            {
                var getActiveForm = _formDal.GetList(x => x.Status == (int)SupportFormStatusEnum.Active);
                if (getActiveForm.Count == 0)
                {
                    return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), "active not found", Messages.err_null);
                }

                if (userId.HasValue)
                {
                    getActiveForm = getActiveForm.Where(x => x.UserId == userId.Value).ToList(); 
                }

                var list = new List<SupportFormListDto>();
                foreach (var form in getActiveForm)
                {
                    var formUser = _userDal.Get(x => x.Id == form.UserId);
                    string userName = formUser != null ? formUser.Name : string.Empty;
                    string userSurname = formUser != null ? formUser.Surname : string.Empty;
                    list.Add(new SupportFormListDto
                    {
                        FormId = form.Id,
                        Subject = form.Subject,
                        Message = form.Message,
                        Status = ((SupportFormStatusEnum)form.Status).ToString(),
                        UserName = userName,
                        UserSurname = userSurname,
                        CreatedDate = form.CreatedDate,
                    });
                }

                return new SuccessDataResult<List<SupportFormListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), ex.Message, Messages.err_null);
            }
        }




        public IDataResult<List<SupportFormListDto>> GetCancelledSupportList(int? userId)
        {
            try
            {
                var getCancelledForm = _formDal.GetList(x => x.Status == (int)SupportFormStatusEnum.Canceled);
                if (getCancelledForm.Count == 0)
                {
                    return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), "cancelled not found", Messages.err_null);
                }

                if (userId.HasValue)
                {
                    getCancelledForm = getCancelledForm.Where(x => x.UserId == userId.Value).ToList();
                }

                var list = new List<SupportFormListDto>();
                foreach (var form in getCancelledForm)
                {
                    var formUser = _userDal.Get(x => x.Id == form.UserId);
                    string userName = formUser != null ? formUser.Name : string.Empty;
                    string userSurname = formUser != null ? formUser.Surname : string.Empty;
                    list.Add(new SupportFormListDto
                    {
                        FormId = form.Id,
                        Subject = form.Subject,
                        Message = form.Message,
                        Status = ((SupportFormStatusEnum)form.Status).ToString(),
                        UserName = userName,
                        UserSurname = userSurname,
                        CreatedDate = form.CreatedDate,
                    });
                }
                return new SuccessDataResult<List<SupportFormListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<SupportFormListDto>> GetList()
        {
            try
            {
                var getSupportForms = _formDal.GetList().ToList();
                if (getSupportForms.Count == 0)
                {
                    return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), "SupportForms not found", Messages.err_null);
                }
                var list = new List<SupportFormListDto>();
                foreach (var form in getSupportForms)
                {
                    var formUser = _userDal.Get(x => x.Id == form.UserId);
                    string userName = formUser != null ? formUser.Name : string.Empty;
                    string userSurname = formUser != null ? formUser.Surname : string.Empty;
                    list.Add(new SupportFormListDto
                    {
                        FormId = form.Id,
                        Subject = form.Subject,
                        Message = form.Message,
                        Status = ((SupportFormStatusEnum)form.Status).ToString(),
                        UserName = userName,
                        UserSurname = userSurname,
                        CreatedDate = form.CreatedDate,
                    });
                }
                return new SuccessDataResult<List<SupportFormListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<SupportFormListDto>> GetSupportFormsByUserId(int userId)
        {
            try
            {
                var getUser = _userDal.Get(x => x.Id == userId);


                if (getUser == null)
                {
                    return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), "User not found", Messages.err_null);
                }

                var getSupportForms = _formDal.GetList(x => x.UserId == userId).ToList();

                var list = getSupportForms.Select(form => new SupportFormListDto
                {
                    Subject = form.Subject,
                    Message = form.Message,
                    Status = ((SupportFormStatusEnum)form.Status).ToString(),
                    UserName = getUser.Name,
                    UserSurname = getUser.Surname,
                    CreatedDate = form.CreatedDate,
                }).ToList();

                return new SuccessDataResult<List<SupportFormListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), ex.Message, Messages.err_null);
            }
        }



        public IDataResult<List<SupportFormListDto>> GetWaitingSupportList(int? userId)
        {
            try
            {
                var getWaitingForm = _formDal.GetList(x => x.Status == (int)SupportFormStatusEnum.Waiting);
                if (getWaitingForm.Count == 0)
                {
                    return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), "waiting not found", Messages.err_null);
                }


                if (userId.HasValue)
                {
                    getWaitingForm = getWaitingForm.Where(x => x.UserId == userId.Value).ToList();
                }

                var list = new List<SupportFormListDto>();
                foreach (var form in getWaitingForm)
                {
                    var formUser = _userDal.Get(x => x.Id == form.UserId);
                    string userName = formUser != null ? formUser.Name : string.Empty;
                    string userSurname = formUser != null ? formUser.Surname : string.Empty;
                    list.Add(new SupportFormListDto
                    {
                        Subject = form.Subject,
                        Message = form.Message,
                        Status = ((SupportFormStatusEnum)form.Status).ToString(),
                        UserName = userName,
                        UserSurname = userSurname,
                        CreatedDate = form.CreatedDate,
                    });
                }
                return new SuccessDataResult<List<SupportFormListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<SupportFormListDto>>(new List<SupportFormListDto>(), ex.Message, Messages.err_null);
            }
        }

        public IDataResult<bool> Update(SupportFormUpdateDto supportFormUpdateDto)
        {
            try
            {
                var getForm = _formDal.Get(x => x.Id == supportFormUpdateDto.SupportFormId);

                if (getForm == null)
                {
                    return new ErrorDataResult<bool>(false, "Support form not found", Messages.err_null);
                }

                getForm.Subject = supportFormUpdateDto.Subject;
                getForm.Message = supportFormUpdateDto.Message;

                _formDal.Update(getForm);
                return new SuccessDataResult<bool>(true, "Support form updated successfully", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }

    }
}

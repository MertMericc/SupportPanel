using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.BLL.Abstract;
using Support.Entity.DTO.UserDtos;
using System.Data;

namespace Support.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ISupportFormService _supportFormService;


        public AdminController(IAuthService authService, IUserService userService, ISupportFormService supportFormService)
        {
            _authService = authService;
            _userService = userService;
            _supportFormService = supportFormService;
        }

        [HttpPost("registerforadmin")]
        public IActionResult Register(UserRegisterAdminstratorDto userRegisterDto)
        {
            var userExists = _authService.UserExists(userRegisterDto.Email);
            if (!userExists.Success)
            {
                return Ok(userExists);
            }
            var result = _authService.RegisterForAdmin(userRegisterDto);
            return Ok(result);
        }


        [HttpGet("getuserlist")]
        public IActionResult GetUserList()
        {
            var result = _userService.GetList();
            return Ok(result);
        }

        [HttpPost("changestatususer")]
        public IActionResult UpdateUser(int userId)
        {
            var result = _userService.ChangeStatus(userId);
            return Ok(result);
        }


        [HttpPost("changestatusform")]
        public IActionResult ChangeStatusForm(int formId, int statusId)
        {
            var result = _supportFormService.ChangeStatus(formId, statusId);
            return Ok(result);
        }

        [HttpPost("getformswithuserid")]
        public IActionResult GetFormsWithUserId(int userId)
        {
            var result = _supportFormService.GetSupportFormsByUserId(userId);
            return Ok(result);
        }

        [HttpGet("getlistform")]
        public IActionResult GetFormList()
        {
            var result = _supportFormService.GetList();
            return Ok(result);
        }

        [HttpPost("getactiveforms")]
        public IActionResult GetActiveForms(int? userId)
        {
            var result = _supportFormService.GetActiveSupportList(userId);
            return Ok(result);
        }

        [HttpGet("getwaitingforms")]
        public IActionResult GetWaitingForms(int? userId)
        {
            var result = _supportFormService.GetWaitingSupportList(userId);
            return Ok(result);
        }

        [HttpGet("getcancelledforms")]
        public IActionResult GetCancelledForms(int? userId)
        {
            var result = _supportFormService.GetCancelledSupportList(userId);
            return Ok(result);
        }
    }
}

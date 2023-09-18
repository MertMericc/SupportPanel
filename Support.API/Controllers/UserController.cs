using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.BLL.Abstract;
using Support.Entity.DTO.UserDtos;

namespace Support.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("updateuser")]
        public IActionResult UpdateUser(UserUpdateDto userUpdateDto)
        {
            var result = _userService.UpdateUser(userUpdateDto);
            return Ok(result);
        }



    }
}

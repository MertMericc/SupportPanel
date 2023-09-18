using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.BLL.Abstract;
using Support.Entity.DTO.UserDtos;

namespace Support.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var result = _authService.Login(userLoginDto);
            return Ok(result);
        }

      
    }
}

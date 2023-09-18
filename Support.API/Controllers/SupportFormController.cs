using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.BLL.Abstract;
using Support.Entity.DTO.SupportFormDtos;
using System.Data;

namespace Support.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member")]

    public class SupportFormController : ControllerBase
    {
        private readonly ISupportFormService _supportFormService;

        public SupportFormController(ISupportFormService supportFormService)
        {
            _supportFormService = supportFormService;
        }

        [HttpPost("addform")]
        public IActionResult AddForm(SupportFormAddDto supportFormAddDto)
        {
            var result = _supportFormService.Add(supportFormAddDto);
            return Ok(result);
        }

        [HttpPost("updateform")]
        public IActionResult UpdateForm(SupportFormUpdateDto supportFormUpdateDto)
        {
            var result = _supportFormService.Update(supportFormUpdateDto);
            return Ok(result);
        }

      

        
    }
}

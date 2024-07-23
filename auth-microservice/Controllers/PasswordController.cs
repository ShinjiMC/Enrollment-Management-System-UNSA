using Microsoft.AspNetCore.Mvc;
using auth_microservice.Services;
using auth_microservice.Models;

namespace auth_microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost("reset")]
        public IActionResult ResetPassword([FromBody] PasswordResetDto passwordResetDto)
        {
            var result = _passwordService.ResetPassword(passwordResetDto);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpPost("change")]
        public IActionResult ChangePassword([FromBody] PasswordChangeDto passwordChangeDto)
        {
            var result = _passwordService.ChangePassword(passwordChangeDto);
            if (!result)
                return BadRequest();
            return Ok();
        }
    }
}

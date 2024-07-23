using Microsoft.AspNetCore.Mvc;
using auth_microservice.Services;
using auth_microservice.Models;

namespace auth_microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = _authService.Login(userLoginDto);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = _authService.Register(userRegisterDto);
            if (!result)
                return BadRequest();
            return Ok();
        }
    }
}

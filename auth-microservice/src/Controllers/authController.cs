using Microsoft.AspNetCore.Mvc;
namespace auth_microservice.Application.DTOs;



[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthRepository userAccount;

    public AuthController(IAuthRepository userAccount)
    {
        this.userAccount = userAccount;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDTO)
    {
        var response = await userAccount.CreateAccount(userDTO);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDTO)
    {
        var response = await userAccount.LoginAccount(loginDTO);
        return Ok(response);
    }
    
}


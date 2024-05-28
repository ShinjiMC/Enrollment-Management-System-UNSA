using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using users_microservice.DTOs;
using users_microservice.repositories;

namespace users_microservice.controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthRepository userAccount;

    // Constructor
    public AuthController(IAuthRepository userAccount)
    {
        this.userAccount = userAccount;
    }

    // Endpoints
    [HttpPost("register")]
    [Authorize(Roles = "Admin")] // Only Admin can register new users
    public async Task<IActionResult> Register(UserDto userDTO)
    {
        var response = await userAccount.CreateAccount(userDTO);
        return Ok(response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDTO)
    {
        var response = await userAccount.LoginAccount(loginDTO);
        return Ok(response);
    }
    
}


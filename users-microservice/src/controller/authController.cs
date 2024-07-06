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

    [HttpPost("register/admin")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto registerAdminDto)
    {
        var userExists = await userAccount.FindByEmailAsync(registerAdminDto.Email);
        if (userExists != null)
        {
            return BadRequest("User already exists");
        }

        var result = await userAccount.RegisterAdmin(registerAdminDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Admin registered successfully");
    }
    [HttpPost("register/student")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto registerStudentDto)
    {
        var userExists = await userAccount.FindByEmailAsync(registerStudentDto.Email);
        if (userExists != null)
        {
            return BadRequest("User already exists");
        }
        var result = await userAccount.RegisterStudent(registerStudentDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok("Student registered successfully");
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDTO)
    {
        var response = await userAccount.LoginAccount(loginDTO);
        return Ok(response);
    }
}


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
    private readonly IAdminRepository adminAccount;
    private readonly IStudentRepository studentAccount;

    // Constructor
    public AuthController(IAuthRepository userAccount, IAdminRepository adminRepository, IStudentRepository studentAccount)
    {
        this.userAccount = userAccount;
        this.adminAccount = adminRepository;
        this.studentAccount = studentAccount;
    }

    // Endpoints
    [HttpPost("register")]
    // [Authorize(Roles = "ADMIN")] // Only Admin can register new users
    public async Task<IActionResult> Register(UserDto userDTO)
    {
        var response = await adminAccount.CreateAdminAccount(userDTO);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDTO)
    {
        var response = await userAccount.LoginAccount(loginDTO);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("new-student")]
    [Authorize(Roles = "ADMIN")] // Only Admin can register new students
    public async Task<IActionResult> RegisterNewStudent(StudentDto studentDTO)
    {
        var response = await studentAccount.CreateStudentAccount(studentDTO);
        return StatusCode(response.StatusCode, response);
    }
}

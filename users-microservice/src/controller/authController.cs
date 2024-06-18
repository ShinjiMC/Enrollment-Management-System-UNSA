using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using users_microservice.DTOs;
using users_microservice.services;

namespace users_microservice.controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAdminService _adminService;
    private readonly IStudentService _studentService;

    // Constructor
    public AuthController(IAuthService authService, IAdminService adminRepository, IStudentService studentService)
    {
        _authService = authService;
        _adminService = adminRepository;
        _studentService = studentService;
    }

    // Endpoints
    [HttpPost("register")]
    // [Authorize(Roles = "ADMIN")] // Only Admin can register new users
    public async Task<IActionResult> Register(UserDto userDTO)
    {
        var response = await _adminService.CreateAdminAccount(userDTO);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDTO)
    {
        var response = await _authService.LoginAccount(loginDTO);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("new-student")]
    [Authorize(Roles = "ADMIN")] // Only Admin can register new students
    public async Task<IActionResult> RegisterNewStudent(StudentDto studentDTO)
    {
        var response = await _studentService.CreateStudentAccount(studentDTO);
        return StatusCode(response.StatusCode, response);
    }
}

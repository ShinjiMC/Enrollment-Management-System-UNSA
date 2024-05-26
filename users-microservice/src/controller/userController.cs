using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;

using System.Text.Json; // Asegúrate de tener esta línea al inicio del archivo
namespace users_microservice.controllers;

public interface IUserController
{
    Task<IActionResult> CreateUser(UserModel user);
    Task<IActionResult> GetUsers();
}

[ApiController]
[Route("api/[controller]")]


public class UserController : ControllerBase, IUserController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }


[HttpPost("create")]
public async Task<IActionResult> CreateUser(UserModel user)
{
    if (user == null)
    {
        return BadRequest("User data is required");
    }

    Console.WriteLine("Received User: " + JsonSerializer.Serialize(user));

    switch (user.Role)
    {
        case Role.ADMIN:
            var admin = user as Admin;
            Console.WriteLine("Processed as Admin: " + JsonSerializer.Serialize(admin));
            if (admin == null || string.IsNullOrEmpty(admin.Department))
            {
                return BadRequest("Admin-specific data is missing");
            }
            await _userService.CreateUser(admin);
            break;

        case Role.STUDENT:
            var student = user as Student;
            if (student == null || string.IsNullOrEmpty(student.CUI) || string.IsNullOrEmpty(student.Email))
            {
                return BadRequest("Student-specific data is missing");
            }
            Console.WriteLine("Processed as Student: " + JsonSerializer.Serialize(student));
            await _userService.CreateUser(student);
            break;

        default:
            return BadRequest("Invalid user role");
    }

    return Ok(user);
}



    [HttpGet("all")]
    public async Task<IActionResult> GetUsers() 
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }
}

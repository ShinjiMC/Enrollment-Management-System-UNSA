using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace users_microservice.controllers;

public interface IUserController
{
    Task<IActionResult> GetUsers();
}

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase, IUserController
{
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers() 
    {
        await Task.Delay(100);
        return Ok("All users");
    }

    [HttpGet("student")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetInfoStudent()
    {
        await Task.Delay(100);
        return Ok("This is info about one student");
    }
}
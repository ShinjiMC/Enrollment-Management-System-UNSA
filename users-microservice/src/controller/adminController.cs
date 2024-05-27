using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;
namespace users_microservice.controllers;

public interface IAdminController
{
    Task<IActionResult> CreateUser(AdminModel user);
    Task<IActionResult> GetUsers();
}

[ApiController]
[Route("api/[controller]")]

public class AdminController : ControllerBase, IAdminController
{
    private readonly IAdminService _userService;
    public AdminController(IAdminService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(AdminModel user)
    {
        if (user == null)
        {
            return BadRequest("User data is required");
        }
        var createdUser = await _userService.CreateUser(user);
        return Ok(createdUser);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }
}

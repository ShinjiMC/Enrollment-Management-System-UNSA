using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;

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

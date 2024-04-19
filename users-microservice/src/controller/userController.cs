using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;

namespace users_microservice.controllers;

public interface IUserController
{
    IActionResult CreateUser(UserModel user);
    IActionResult GetUsers();
}

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase, IUserController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("api/users/create")]
    public IActionResult CreateUser(UserModel user)
    {
        var createdUser = _userService.CreateUser(user);
        return Ok(createdUser);
    }
    
    [HttpGet("api/users")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using users_microservice.authentication;
using users_microservice.services;

namespace users_microservice.controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthenticationController(ILoginService loginService) =>
        _loginService = loginService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _loginService.SignInAsync(request.userId, request.username);
        return Ok(new LoginResponse { Token = token });
    }
}
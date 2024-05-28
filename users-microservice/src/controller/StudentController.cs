using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;

namespace users_microservice.controllers;

public interface IStudentController
{
    Task<IActionResult> GetAllStudents();
}


[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase, IStudentController
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllStudents() 
    {
        var users = await _studentService.GetAllStudents();
        return Ok(users);
    }
}
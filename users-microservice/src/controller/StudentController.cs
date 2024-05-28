
using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;

namespace users_microservice.controllers;

public interface IStudentController
{
    IEnumerable<StudentModel> GetAllStudents();
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
    public IEnumerable<StudentModel> GetAllStudents()
    {
        var users = _studentService.GetAllStudents();
        return users;
    }
}
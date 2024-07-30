
using Microsoft.AspNetCore.Mvc;
using users_microservice.Application.Service.Interface;


namespace users_microservice.controllers;

public interface IStudentController
{
    Task<IActionResult> GetStudentById(string id);
    Task<IActionResult> GetCoursesByStudentId(string id);
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

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(string id)
    {
        var student = await _studentService.GetStudentAsync(id);
        if (student == null)
            return NotFound();
        
        if (string.IsNullOrEmpty(student.FullName))
            return NotFound();

        return Ok(student);
    }

    [HttpGet("{id}/courses")]
    public async Task<IActionResult> GetCoursesByStudentId(string id)
    {
        var courses = await _studentService.GetCoursesByStudentIdAsync(id);
        
        if(courses == null)
            return NotFound();
        
        if (string.IsNullOrEmpty(courses.FullName))
            return NotFound();

        return Ok(courses);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllStudents() 
    {
        var users = await _studentService.GetAllStudents();
        
        return Ok(users);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using users_microservice.Application.Dtos;
using users_microservice.Application.Service.Interface;


namespace users_microservice.controllers;

public interface IAdminController
{
    Task<IActionResult> GetAllStudents();
}


[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase, IAdminController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register/student")]
    public async Task<IActionResult> RegisterStudent([FromBody] StudentDto student)
    {
        var createdStudent = await _adminService.CreateStudentAsync(student);
        return StatusCode(createdStudent.StatusCode, createdStudent);
    }
    
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] AdminDto userDTO)
    {
        var response = await _adminService.CreateAdminAccountAsync(userDTO);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("admin/{id}")]
    public async Task<IActionResult> UpdateAdmin([FromBody] AdminDto admin)
    {
        var updatedAdmin = await _adminService.UpdateAdminAccountAsync(admin);
        if (updatedAdmin == null)
            return NotFound();

        return StatusCode(updatedAdmin.StatusCode, updatedAdmin);
    }

    [HttpDelete("admin/{id}")]
    public async Task<IActionResult> DeleteAdmin(string id)
    {
        var result = await _adminService.DeleteAdminAccountAsync(id);
        if (!result.Flag)
            return NotFound();

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("admin/{id}")]
    public async Task<IActionResult> GetAdmin(string id)
    {
        var admin = await _adminService.RetrieveAdminAccountAsync(id);
        if (admin == null)
            return NotFound();

        if (string.IsNullOrEmpty(admin.FullName) )
            return NotFound();

        return Ok(admin);
    }

    [HttpGet("admin/all")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var admin = await _adminService.GetAllAdminsAsync();
        if (admin == null)
            return NotFound();

        return Ok(admin);
    }
    
    [HttpGet("student/{id}")]
    public async Task<IActionResult> GetStudent(string id)
    {
        var student = await _adminService.GetStudentAsync(id);
        if (student == null)
            return NotFound();
        
        if (string.IsNullOrEmpty(student.FullName) )
            return NotFound();

        return Ok(student);
    }

    [HttpGet("student/{id}/courses")]
    public async Task<IActionResult> GetCoursesByStudentId(string id)
    {
        var courses = await _adminService.GetCoursesByStudentIdAsync(id);

        if (string.IsNullOrEmpty(courses.FullName) )
            return NotFound();
        return Ok(courses);
    }
    [HttpPut("student/{id}")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentDto student)
    {
        var updatedStudent = await _adminService.UpdateStudentAsync(id, student);
        if (updatedStudent == null)
            return NotFound();
        

        return Ok(updatedStudent);
    }

    [HttpDelete("student/{id}")]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        var result = await _adminService.DeleteStudentAsync(id);
        if (!result.Flag)
            return NotFound();

        return NoContent();
    }

    [HttpGet("student")]
    public async Task<IActionResult> GetAllStudents()
    {
        var result = await _adminService.GetAllStudentsAsync();
        if (result.IsNullOrEmpty())
            return NotFound();

        return NoContent();
    }
}
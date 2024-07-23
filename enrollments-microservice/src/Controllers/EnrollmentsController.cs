using enrollments_microservice.Application.Dtos;
using enrollments_microservice.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace enrollments_microservice.Controllers;

public interface IEnrollmentsController
{
    Task<IActionResult> GetAvailableCourses(string userId, string schoolId);
    Task<IActionResult> GetEnrollmentsByUserId(string userId);
    Task<IActionResult> GetEnrollmentsBySchoolId(string schoolId);
    Task<IActionResult> GetEnrollmentByUserIdAndSchoolId(string userId, string schoolId);
    Task<IActionResult> GetEnrollmentById(string enrollId);
    Task<IActionResult> CreateEnrollment(string userId, string schoolId, [FromBody] EnrollmentDto enrollmentDto);
    Task<IActionResult> UpdateEnrollment(string id, [FromBody] EnrollmentDto enrollmentDto);
    Task<IActionResult> DeleteEnrollment(string enrollId);
}

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase, IEnrollmentsController
{
    private readonly IEnrollService _enrollService;

    public EnrollmentsController(IEnrollService enrollService)
    {
        _enrollService = enrollService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetEnrollments()
    {
        var result = await _enrollService.GetEnrollments();
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("available/{userId}/{schoolId}")]
    public async Task<IActionResult> GetAvailableCourses(string userId, string schoolId)
    {
        var result = await _enrollService.GetAvailableCourses(userId, schoolId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetEnrollmentsByUserId(string userId)
    {
        var result = await _enrollService.GetEnrollmentsByUserId(userId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("school/{schoolId}")]
    public async Task<IActionResult> GetEnrollmentsBySchoolId(string schoolId)
    {
        var result = await _enrollService.GetEnrollmentsBySchoolId(schoolId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("certificate/{userId}/{schoolId}")]
    public async Task<IActionResult> GetEnrollmentByUserIdAndSchoolId(string userId, string schoolId)
    {
        var result = await _enrollService.GetEnrollmentByUserIdAndSchoolId(userId, schoolId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{enrollId}")]
    public async Task<IActionResult> GetEnrollmentById(string enrollId)
    {
        var result = await _enrollService.GetEnrollmentById(enrollId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("enroll/{userId}/{schoolId}")]
    public async Task<IActionResult> CreateEnrollment(string userId, string schoolId, [FromBody] EnrollmentDto enrollmentDto)
    {
        var result = await _enrollService.CreateEnrollment(userId, schoolId, enrollmentDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("enroll/{id}")]
    public async Task<IActionResult> UpdateEnrollment(string id, [FromBody] EnrollmentDto enrollmentDto)
    {
        var result = await _enrollService.UpdateEnrollment(id, enrollmentDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("enroll/{enrollId}")]
    public async Task<IActionResult> DeleteEnrollment(string enrollId)
    {
        var result = await _enrollService.DeleteEnrollment(enrollId);
        if (!result.Flag) return BadRequest();
        return Ok(result);
    }
}
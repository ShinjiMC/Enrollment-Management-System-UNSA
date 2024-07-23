
using Microsoft.AspNetCore.Mvc;
using users_microservice.Application.Service.Interface;


namespace users_microservice.controllers;

public interface IExternalServiceController
{
    Task<IActionResult> ExternalServiceStudentCourse([FromBody] string id);
}


[Route("api/[controller]")]
[ApiController]
public class ExternalServiceController : ControllerBase, IExternalServiceController
{
    private readonly IExternalService _externalService;

    public ExternalServiceController( IExternalService externalService)
    {
        _externalService = externalService;
    }

    
    [HttpGet("student/{id}")]
    public async Task<IActionResult> ExternalServiceStudentCourse(string id) 
    {
        var users = await _externalService.GetStudentCourseData(id);
        return Ok(users);
    }
}
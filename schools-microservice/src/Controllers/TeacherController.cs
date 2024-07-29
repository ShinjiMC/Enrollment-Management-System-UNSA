using Microsoft.AspNetCore.Mvc;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Teacher>> GetAllTeachers()
    {
        return Ok(_teacherService.GetAllTeachers());
    }

    [HttpGet("{id}")]
    public ActionResult<Teacher> GetTeacherById(int id)
    {
        var teacher = _teacherService.GetTeacherById(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return Ok(teacher);
    }

    [HttpPost]
    public ActionResult AddTeacher(Teacher teacher)
    {
        _teacherService.AddTeacher(teacher);
        return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateTeacher(int id, Teacher teacher)
    {
        if (id != teacher.Id)
        {
            return BadRequest();
        }

        _teacherService.UpdateTeacher(teacher);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTeacher(int id)
    {
        _teacherService.DeleteTeacher(id);
        return NoContent();
    }
}

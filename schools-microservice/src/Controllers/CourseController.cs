using Microsoft.AspNetCore.Mvc;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        return Ok(_courseService.GetAllCourses());
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetCourseById(int id)
    {
        var course = _courseService.GetCourseById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpPost]
    public ActionResult AddCourse(Course course)
    {
        _courseService.AddCourse(course);
        return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCourse(int id, Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        _courseService.UpdateCourse(course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCourse(int id)
    {
        _courseService.DeleteCourse(id);
        return NoContent();
    }
}

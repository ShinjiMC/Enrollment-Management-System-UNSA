using Microsoft.AspNetCore.Mvc;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

[Route("api/[controller]")]
[ApiController]
public class StudyPlanCourseController : ControllerBase
{
    private readonly IStudyPlanCourseService _studyPlanCourseService;

    public StudyPlanCourseController(IStudyPlanCourseService studyPlanCourseService)
    {
        _studyPlanCourseService = studyPlanCourseService;
    }

    [HttpGet("{courseId}")]
    public ActionResult<StudyPlanCourse> GetStudyPlanById(int courseId)
    {
        var course = _studyPlanCourseService.GetStudyPlanById(courseId);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpGet]
    public ActionResult<List<StudyPlanCourse>> GetAllStudyPlans()
    {
        var courses = _studyPlanCourseService.GetAllStudyPlans();
        return Ok(courses);
    }

    [HttpPost]
    public ActionResult AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
    {
        _studyPlanCourseService.AddStudyPlanCourse(studyPlanCourse);
        return CreatedAtAction(nameof(GetStudyPlanById), new { id = studyPlanCourse.Id }, studyPlanCourse);
    }

    

}

using Microsoft.AspNetCore.Mvc;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

namespace YourNamespace.Controllers
{
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
        public ActionResult<StudyPlanCourse> GetCourseWithPrerequisites(int courseId)
        {
            var course = _studyPlanCourseService.GetCourseWithPrerequisites(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpGet]
        public ActionResult<List<StudyPlanCourse>> GetAllCoursesWithPrerequisites()
        {
            var courses = _studyPlanCourseService.GetAllCoursesWithPrerequisites();
            return Ok(courses);
        }

        [HttpPost]
        public ActionResult AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
        {
            _studyPlanCourseService.AddStudyPlanCourse(studyPlanCourse);
            return CreatedAtAction(nameof(GetCourseWithPrerequisites), new { id = studyPlanCourse.Id }, studyPlanCourse);
        }

        
    }
}

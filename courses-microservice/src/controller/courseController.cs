using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using course_microservice.models;
using course_microservice.services;
using System.Threading.Tasks;
using course_microservice.DTOs;

namespace course_microservice.controllers
{
    public interface ICourseController
    {
        Task<IActionResult> GetAllCourses();
        Task<IActionResult> GetCourse(int id);
        Task<IActionResult> AddCourse(CourseDto courseDto);
        Task<IActionResult> UpdateCourse(int id, CourseDto courseDto);
        Task<IActionResult> DeleteCourse(int id);
        Task<IActionResult> GetCoursePrerequisites(int courseId);
        Task<IActionResult> AddCoursePrerequisite(int courseId, int prerequisiteCourseId);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase, ICourseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCourse(CourseDto courseDto)
        {
            var courseModel = ConvertToCourseModel(courseDto);
            var addedCourse = await _courseService.AddCourse(courseModel);
            return CreatedAtAction(nameof(GetCourse), new { id = addedCourse.ID }, addedCourse);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto courseDto)
        {
            var courseModel = ConvertToCourseModel(courseDto);
            var updatedCourse = await _courseService.UpdateCourse(id, courseModel);
            if (updatedCourse == null)
            {
                return NotFound();
            }
            return Ok(updatedCourse);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{courseId}/prerequisites")]
        public async Task<IActionResult> GetCoursePrerequisites(int courseId)
        {
            var prerequisites = await _courseService.GetCoursePrerequisites(courseId);
            return Ok(prerequisites);
        }

        [HttpPost("prerequisites")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCoursePrerequisite(int courseId, int prerequisiteCourseId)
        {
            var addedPrerequisite = await _courseService.AddCoursePrerequisite(courseId, prerequisiteCourseId);
            if (addedPrerequisite == null)
            {
                return BadRequest("Prerequisite could not be added.");
            }
            return CreatedAtAction(nameof(GetCoursePrerequisites), new { courseId = courseId }, addedPrerequisite);
        }

        private CourseModel ConvertToCourseModel(CourseDto courseDto)
        {
            return new CourseModel
            {
                ID = courseDto.ID,
                Name = courseDto.Name,
                Semester = courseDto.Semester,
                Credits = courseDto.Credits,
                Year = courseDto.Year,
                Hours = courseDto.Hours
            };
        }
    }
}

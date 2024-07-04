using Microsoft.AspNetCore.Mvc;
using course_microservice.DTOs;
using course_microservice.services;
using System.Threading.Tasks;
using course_microservice.models;

namespace course_microservice.controllers
{
    public interface IScheduleController
    {
        Task<IActionResult> GetAllSchedules();
        Task<IActionResult> GetSchedule(int id);
        Task<IActionResult> AddSchedule(ScheduleDto ScheduleModel);
        Task<IActionResult> UpdateSchedule(int id, ScheduleDto ScheduleModel);
        Task<IActionResult> DeleteSchedule(int id);

        Task<IActionResult> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase, IScheduleController
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedules();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedule(ScheduleDto ScheduleModel)
        {
            var scheduleModel = ConvertToScheduleModel(ScheduleModel);
            var addedSchedule = await _scheduleService.AddSchedule(scheduleModel);
            if (addedSchedule == null)
            {
                return BadRequest("A schedule with the same combination of CourseID, DayOfWeekID, SchoolID, Group, and Year already exists.");
            }
            return CreatedAtAction(nameof(GetSchedule), new { id = addedSchedule.ID }, addedSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleDto ScheduleModel)
        {
            var scheduleModel = ConvertToScheduleModel(ScheduleModel);
            var updatedSchedule = await _scheduleService.UpdateSchedule(id, scheduleModel);
            if (updatedSchedule == null)
            {
                return NotFound();
            }
            return Ok(updatedSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var result = await _scheduleService.DeleteSchedule(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("byYearSemesterSchool")]
        public async Task<IActionResult> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID)
        {
            var schedules = await _scheduleService.GetSchedulesByYearSemesterSchool(year, semester, schoolID);
            return Ok(schedules);
        }

        private ScheduleModel ConvertToScheduleModel(ScheduleDto ScheduleDto)
        {
            return new ScheduleModel
            {
                ID = ScheduleDto.ID,
                CourseID = ScheduleDto.CourseID,
                WeekDayID = ScheduleDto.WeekDayID,
                SchoolID = ScheduleDto.SchoolID,
                Group = ScheduleDto.Group,
                Year = ScheduleDto.Year,
                StartTime = ScheduleDto.StartTime,
                EndTime = ScheduleDto.EndTime,
                TeacherFullName = ScheduleDto.TeacherFullName,
                Capacity = ScheduleDto.Capacity
            };
        }
    }
}

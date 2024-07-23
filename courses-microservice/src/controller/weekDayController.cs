using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using course_microservice.models;
using course_microservice.services;
using course_microservice.DTOs;

namespace course_microservice.controllers
{
    public interface IWeekDayController
    {
        Task<IActionResult> GetAllWeekDays();
        Task<IActionResult> GetWeekDay(int id);
        Task<IActionResult> AddWeekDay(WeekDayDto weekDayDto);
        Task<IActionResult> UpdateWeekDay(int id, WeekDayDto weekDayDto);
        Task<IActionResult> DeleteWeekDay(int id);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WeekDayController : ControllerBase, IWeekDayController
    {
        private readonly IWeekDayService _weekDayService;

        public WeekDayController(IWeekDayService weekDayService)
        {
            _weekDayService = weekDayService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWeekDays()
        {
            var weekDays = await _weekDayService.GetAllWeekDays();
            return Ok(weekDays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeekDay(int id)
        {
            var weekDay = await _weekDayService.GetWeekDay(id);
            if (weekDay == null)
            {
                return NotFound();
            }
            return Ok(weekDay);
        }

        [HttpPost]
        public async Task<IActionResult> AddWeekDay(WeekDayDto weekDayDto)
        {
            var weekDayModel = ConvertToWeekDayModel(weekDayDto);
            var addedWeekDay = await _weekDayService.AddWeekDay(weekDayModel);
            return CreatedAtAction(nameof(GetWeekDay), new { id = addedWeekDay.ID }, addedWeekDay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeekDay(int id, WeekDayDto weekDayDto)
        {
            var weekDayModel = ConvertToWeekDayModel(weekDayDto);
            var updatedWeekDay = await _weekDayService.UpdateWeekDay(id, weekDayModel);
            if (updatedWeekDay == null)
            {
                return NotFound();
            }
            return Ok(updatedWeekDay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeekDay(int id)
        {
            var result = await _weekDayService.DeleteWeekDay(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private WeekDayModel ConvertToWeekDayModel(WeekDayDto weekDayDto)
        {
            return new WeekDayModel
            {
                ID = weekDayDto.ID,
                Name = weekDayDto.Name
            };
        }
    }
}

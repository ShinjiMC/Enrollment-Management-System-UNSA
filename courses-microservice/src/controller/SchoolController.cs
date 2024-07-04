using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using course_microservice.models;
using course_microservice.services;
using System.Threading.Tasks;
using course_microservice.DTOs;

namespace course_microservice.controllers
{
    public interface ISchoolController
    {
        Task<IActionResult> GetAllSchools();
        Task<IActionResult> GetSchool(int id);
        Task<IActionResult> AddSchool(SchoolDto schoolDto);
        Task<IActionResult> UpdateSchool(int id, SchoolDto schoolDto);
        Task<IActionResult> DeleteSchool(int id);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase, ISchoolController
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchools();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSchool(int id)
        {
            var school = await _schoolService.GetSchool(id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSchool(SchoolDto schoolDto)
        {
            var schoolModel = ConvertToSchoolModel(schoolDto);
            var addedSchool = await _schoolService.AddSchool(schoolModel);
            return CreatedAtAction(nameof(GetSchool), new { id = addedSchool.ID }, addedSchool);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSchool(int id, SchoolDto schoolDto)
        {
            var schoolModel = ConvertToSchoolModel(schoolDto);
            var updatedSchool = await _schoolService.UpdateSchool(id, schoolModel);
            if (updatedSchool == null)
            {
                return NotFound();
            }
            return Ok(updatedSchool);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var result = await _schoolService.DeleteSchool(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private SchoolModel ConvertToSchoolModel(SchoolDto schoolDto)
        {
            return new SchoolModel
            {
                ID = schoolDto.ID,
                Name = schoolDto.Name,
                Faculty = schoolDto.Faculty,
                Area = schoolDto.Area,
                FoundationDate = schoolDto.FoundationDate
            };
        }
    }
}

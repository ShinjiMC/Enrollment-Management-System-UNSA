using Microsoft.AspNetCore.Mvc;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

[ApiController]
[Route("api/[controller]")]
public class StudyPlanSchoolController : ControllerBase
{
    private readonly IStudyPlanSchoolService _studyPlanSchoolService;

    public StudyPlanSchoolController(IStudyPlanSchoolService studyPlanSchoolService)
    {
        _studyPlanSchoolService = studyPlanSchoolService;
    }

    [HttpPost]
    public IActionResult AddStudyPlanSchool([FromBody] StudyPlanSchool studyPlanSchool)
    {
        _studyPlanSchoolService.AddStudyPlanSchool(studyPlanSchool);
        return Ok();
    }

    [HttpGet("{schoolName}")]
    public ActionResult<StudyPlanSchool> GetStudyPlanBySchoolName(string schoolName)
    {
        var studyPlanSchool = _studyPlanSchoolService.GetStudyPlanBySchoolName(schoolName);
        if (studyPlanSchool == null)
        {
            return NotFound();
        }
        return Ok(studyPlanSchool);
    }

    [HttpGet("byId/{schoolId}")]
    public ActionResult<StudyPlanSchool> GetStudyPlanBySchoolId(int schoolId)
    {
        var studyPlanSchool = _studyPlanSchoolService.GetStudyPlanBySchoolId(schoolId);
        if (studyPlanSchool == null)
        {
            return NotFound();
        }
        return Ok(studyPlanSchool);
    }
}



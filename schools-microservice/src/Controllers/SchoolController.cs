using Microsoft.AspNetCore.Mvc;//para desarrollar aplicaciones ASP.NET MVC
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Service;

[ApiController] //controlador de API web
[Route("api/[controller]")] //ruta base api/schools
public class SchoolController : ControllerBase
{
    
    private readonly ISchoolService _schoolService;

    //Métodos del controlador
    public SchoolController(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    //GetAllSchools maneja solicitudes HTTP GET.
    [HttpGet]//api/schools
    public ActionResult<IEnumerable<School>> GetAllSchools()
    {
        return Ok(_schoolService.GetAllSchools());//devuelve estado 200 http
    }


    //GetSchoolById maneja solicitudes HTTP GET con un identificador (id)
    [HttpGet("{id}")]//api/schools/{id}
    public ActionResult<School> GetSchoolById(int id)
    {
        var school = _schoolService.GetSchoolById(id);
        if (school == null)
        {
            return NotFound();
        }
        return Ok(school);
    }


    //api/schools
    [HttpPost]
    public ActionResult AddSchool(School school)
    {
        _schoolService.AddSchool(school);
        return CreatedAtAction(nameof(GetSchoolById), new { id = school.Id }, school);
    }

    [HttpPut("{id}")]//api/schools/{id}
    public ActionResult UpdateSchool(int id, School school)
    {
        if (id != school.Id)
        {
            return BadRequest();
        }

        _schoolService.UpdateSchool(school);
        return NoContent();
    }

    [HttpDelete("{id}")]//api/schools/{id}
    public ActionResult DeleteSchool(int id)
    {
        _schoolService.DeleteSchool(id);
        return NoContent();//caso exitoso
    }
}

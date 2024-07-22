using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Department>> GetAllDepartments()
    {
        return Ok(_departmentService.GetAllDepartments());
    }

    [HttpGet("{id}")]
    public ActionResult<Department> GetDepartmentById(int id)
    {
        var department = _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public ActionResult AddDepartment(Department department)
    {
        _departmentService.AddDepartment(department);
        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateDepartment(int id, Department department)
    {
        if (id != department.Id)
        {
            return BadRequest();
        }

        _departmentService.UpdateDepartment(department);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDepartment(int id)
    {
        _departmentService.DeleteDepartment(id);
        return NoContent();
    }
}

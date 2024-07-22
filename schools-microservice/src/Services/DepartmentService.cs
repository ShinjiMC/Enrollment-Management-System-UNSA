// Services/DepartmentService.cs
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public IEnumerable<Department> GetAllDepartments()
    {
        return _departmentRepository.GetAllDepartments();
    }

    public Department GetDepartmentById(int id)
    {
        return _departmentRepository.GetDepartmentById(id);
    }

    public void AddDepartment(Department department)
    {
        _departmentRepository.AddDepartment(department);
    }

    public void UpdateDepartment(Department department)
    {
        _departmentRepository.UpdateDepartment(department);
    }

    public void DeleteDepartment(int id)
    {
        _departmentRepository.DeleteDepartment(id);
    }
}

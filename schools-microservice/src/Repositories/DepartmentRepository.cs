public class DepartmentRepository : IDepartmentRepository
{
    private readonly List<Department> _departments = new();

    public IEnumerable<Department> GetAllDepartments()
    {
        return _departments;
    }

    public Department GetDepartmentById(int id)
    {
        return _departments.FirstOrDefault(d => d.Id == id);
    }

    public void AddDepartment(Department department)
    {
        _departments.Add(department);
    }

    public void UpdateDepartment(Department department)
    {
        var existingDepartment = GetDepartmentById(department.Id);
        if (existingDepartment != null)
        {
            existingDepartment.Name = department.Name;
        }
    }

    public void DeleteDepartment(int id)
    {
        var department = GetDepartmentById(id);
        if (department != null)
        {
            _departments.Remove(department);
        }
    }
}

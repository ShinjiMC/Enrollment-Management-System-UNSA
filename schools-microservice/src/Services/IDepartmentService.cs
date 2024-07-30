using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IDepartmentService
{
    IEnumerable<Department> GetAllDepartments();
    Department GetDepartmentById(int id);
    void AddDepartment(Department department);
    void UpdateDepartment(Department department);
    void DeleteDepartment(int id);
}

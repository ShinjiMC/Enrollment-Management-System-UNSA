using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;
using System.Collections.Generic;
using System.Linq;
public class DepartmentRepository : IDepartmentRepository
{
    private readonly MongoDbContext _context;

    public DepartmentRepository(MongoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Department> GetAllDepartments()
    {
        return _context.Departments.Find(department => true).ToList();
    }

    public Department GetDepartmentById(int id)
    {
        return _context.Departments.Find(department => department.Id == id).FirstOrDefault();
    }

    public void AddDepartment(Department department)
    {
        _context.Departments.InsertOne(department);
    }

    public void UpdateDepartment(Department department)
    {
        _context.Departments.ReplaceOne(d => d.Id == department.Id, department);
    }

    public void DeleteDepartment(int id)
    {
        _context.Departments.DeleteOne(department => department.Id == id);
    }
}

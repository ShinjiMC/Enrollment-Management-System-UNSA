using users_microservice.models;

namespace users_microservice.repositories;

public interface IStudentRepository
{
    IEnumerable<StudentModel> GetAllStudents();
}


public class StudentRepository : IStudentRepository
{

    private readonly List<StudentModel> _students = new List<StudentModel>();

    public StudentRepository()
    {
        // Inicialización de datos de ejemplo
        _students.Add(new StudentModel { ID = 1, UserName = "student1", Password = "pass1", FullName = "Student One", Role = Role.STUDENT, CUI = "123456789", Email = "student1@example.com" });
        _students.Add(new StudentModel { ID = 2, UserName = "student2", Password = "pass2", FullName = "Student Two", Role = Role.STUDENT, CUI = "987654321", Email = "student2@example.com" });
    }

    public IEnumerable<StudentModel> GetAllStudents()
    {
        return _students;
    }
}
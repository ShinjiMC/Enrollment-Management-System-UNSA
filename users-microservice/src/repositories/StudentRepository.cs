using users_microservice.models;

namespace users_microservice.repositories;

public interface IStudentRepository
{
    Task<List<StudentModel>> GetAllStudents();
}

public class StudentRepository : IStudentRepository
{

    private readonly List<StudentModel> _students = [];
    public StudentRepository()
    {
        // Inicialización de datos de ejemplo
        _students.Add(new () { 
            ID = 1, 
            UserName = "student1", 
            Password = "pass1", 
            FullName = "Student One", 
            Role = Role.STUDENT, 
            CUI = "123456789", 
            Email = "student1@example.com" });
        _students.Add(new () { 
            ID = 2, 
            UserName = "student2", 
            Password = "pass2", 
            FullName = "Student Two", 
            Role = Role.STUDENT, 
            CUI = "987654321", 
            Email = "student2@example.com" });
    }

    public async Task<List<StudentModel>> GetAllStudents()
    {
        await Task.Delay(1);
        return _students;
    }
}
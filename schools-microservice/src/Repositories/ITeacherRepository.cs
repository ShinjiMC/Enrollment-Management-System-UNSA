using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;
public interface ITeacherRepository
{
    IEnumerable<Teacher> GetAllTeachers();
    Teacher GetTeacherById(int id);
    void AddTeacher(Teacher teacher);
    void UpdateTeacher(Teacher teacher);
    void DeleteTeacher(int id);
}

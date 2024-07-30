using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _teacherRepository.GetAllTeachers();
    }

    public Teacher GetTeacherById(int id)
    {
        return _teacherRepository.GetTeacherById(id);
    }

    public void AddTeacher(Teacher teacher)
    {
        _teacherRepository.AddTeacher(teacher);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        _teacherRepository.UpdateTeacher(teacher);
    }

    public void DeleteTeacher(int id)
    {
        _teacherRepository.DeleteTeacher(id);
    }
}

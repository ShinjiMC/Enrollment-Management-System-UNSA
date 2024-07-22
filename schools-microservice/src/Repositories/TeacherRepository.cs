// Repositories/TeacherRepository.cs
public class TeacherRepository : ITeacherRepository
{
    private readonly List<Teacher> _teachers = new();

    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _teachers;
    }

    public Teacher GetTeacherById(int id)
    {
        return _teachers.FirstOrDefault(t => t.Id == id);
    }

    public void AddTeacher(Teacher teacher)
    {
        _teachers.Add(teacher);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        var existingTeacher = GetTeacherById(teacher.Id);
        if (existingTeacher != null)
        {
            existingTeacher.Name = teacher.Name;
            existingTeacher.Department = teacher.Department;
        }
    }

    public void DeleteTeacher(int id)
    {
        var teacher = GetTeacherById(id);
        if (teacher != null)
        {
            _teachers.Remove(teacher);
        }
    }
}

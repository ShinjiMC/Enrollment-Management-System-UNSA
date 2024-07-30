using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;
public class TeacherRepository : ITeacherRepository
{
    private readonly MongoDbContext _context;

    public TeacherRepository(MongoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _context.Teachers.Find(teacher => true).ToList();
    }

    public Teacher GetTeacherById(int id)
    {
        return _context.Teachers.Find(teacher => teacher.Id == id).FirstOrDefault();
    }

    public void AddTeacher(Teacher teacher)
    {
        _context.Teachers.InsertOne(teacher);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        _context.Teachers.ReplaceOne(t => t.Id == teacher.Id, teacher);
    }

    public void DeleteTeacher(int id)
    {
        _context.Teachers.DeleteOne(teacher => teacher.Id == id);
    }
}

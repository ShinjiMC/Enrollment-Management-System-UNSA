using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;
public class CourseRepository : ICourseRepository
{
    private readonly MongoDbContext _context;

    public CourseRepository(MongoDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Course> GetAllCourses()
    {
        return _context.Courses.Find(course => true).ToList();
    }

    public Course GetCourseById(int id)
    {
        return _context.Courses.Find(course => course.Id == id).FirstOrDefault();
    }

    public void AddCourse(Course course)
    {
        _context.Courses.InsertOne(course);
    }

    public void UpdateCourse(Course course)
    {
        _context.Courses.ReplaceOne(c => c.Id == course.Id, course);
    }

    public void DeleteCourse(int id)
    {
        _context.Courses.DeleteOne(course => course.Id == id);
    }
}

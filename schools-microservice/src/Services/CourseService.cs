using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;
public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _courseRepository.GetAllCourses();
    }

    public Course GetCourseById(int id)
    {
        return _courseRepository.GetCourseById(id);
    }

    public void AddCourse(Course course)
    {
        _courseRepository.AddCourse(course);
    }

    public void UpdateCourse(Course course)
    {
        _courseRepository.UpdateCourse(course);
    }

    public void DeleteCourse(int id)
    {
        _courseRepository.DeleteCourse(id);
    }
}

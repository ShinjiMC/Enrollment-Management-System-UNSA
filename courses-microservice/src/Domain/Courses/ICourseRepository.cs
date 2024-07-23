namespace Domain.Courses;

public interface ICourseRepository
{
    Task<Course> GetByIdAsync(CourseId courseId);
    Task<List<Course>> GetAll();
    void Add(Course course);
    void Update(Course course);
    void Delete(Course course);
}
namespace Domain.Courses;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid courseId);
    Task<List<Course>> GetAll();
    void Add(Course course);
    Task<bool> Update(Course course);
    void Delete(Course course);
}
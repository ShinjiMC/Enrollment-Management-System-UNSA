public class CourseRepository : ICourseRepository
{
    private readonly List<Course> _courses = new();

    public IEnumerable<Course> GetAllCourses()
    {
        return _courses;
    }

    public Course GetCourseById(int id)
    {
        return _courses.FirstOrDefault(c => c.Id == id);
    }

    public void AddCourse(Course course)
    {
        _courses.Add(course);
    }

    public void UpdateCourse(Course course)
    {
        var existingCourse = GetCourseById(course.Id);
        if (existingCourse != null)
        {
            existingCourse.Name = course.Name;
            existingCourse.Credits = course.Credits;
        }
    }

    public void DeleteCourse(int id)
    {
        var course = GetCourseById(id);
        if (course != null)
        {
            _courses.Remove(course);
        }
    }
}

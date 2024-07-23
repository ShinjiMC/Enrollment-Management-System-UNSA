using Domain.ValueObjects;

namespace Domain.Courses;

public sealed class Course : AggregateRoot
{

    public CourseId CourseId { get; private set; }
    public string Name { get; private set; }
    public int Credits { get; private set; }
    public int Hours { get; private set; }
    public bool Active { get; private set; }
    public Semester Semester { get; private set; }
    public int SchoolId { get; private set; }
    private Course(){}
    public Course(
            CourseId courseId,
            string name,
            int credits,
            int hours,
            bool active,
            Semester semester,
            int schoolId)
    {
        CourseId = courseId;
        Name = name;
        Credits = credits;
        Hours = hours;
        Active = active;
        Semester = semester;
        SchoolId = schoolId;
    }

    public static Course UpdateCourse(Guid id,string name,
            int credits,
            int hours,
            bool active,
            Semester semester,
            int schoolId)
    {
        return new Course(new CourseId(id), name, credits, hours,active, semester, schoolId);
    }

}
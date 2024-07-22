namespace enrollments_microservice.Domain.ValueObjects
public class CourseData
{
    public int CourseID { get; private set; }
    public string Name { get; private set; }
    public string Semester { get; private set; }
    public int Credits { get; private set; }
    public List<Schedule> Schedules { get; private set; }

    public CourseData(int courseId, string name, string semester, int credits, List<Schedule> schedules)
    {
        CourseID = courseId;
        Name = name;
        Semester = semester;
        Credits = credits;
        Schedules = schedules;
    }
}
namespace enrollments_microservice.Domain.ValueObjects
public class Schedule
{
    public string Day { get; private set; }
    public string Group { get; private set; }
    public int Year { get; private set; }
    public string TeacherName { get; private set; }
    public List<Interval> Time { get; private set; }

    public Schedule(string day, string group, int year, string teacherName, List<Interval> time)
    {
        Day = day;
        Group = group;
        Year = year;
        TeacherName = teacherName;
        Time = time;
    }
}
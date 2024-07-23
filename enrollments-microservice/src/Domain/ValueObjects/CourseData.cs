namespace enrollments_microservice.Domain.ValueObjects;
using System.Collections.Generic;

public class CourseData
{
    public int CourseID { get; private set; }
    public string Name { get; private set; }
    public string Semester { get; private set; }
    public int Credits { get; private set; }
    public List<ScheduleData> Schedules { get; private set; }

    public CourseData(int courseId, string name, string semester, int credits, List<ScheduleData> schedules)
    {
        if (courseId <= 0)
            throw new ArgumentException("Course ID must be greater than 0 of CourseData");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name must not be empty of CourseData");
        if (string.IsNullOrEmpty(semester))
            throw new ArgumentException("Semester must not be empty of CourseData");
        if (credits <= 0)
            throw new ArgumentException("Credits must be greater than 0 of CourseData");
        if (schedules == null || schedules.Count == 0)
            throw new ArgumentException("Schedules must not be empty of CourseData");
        CourseID = courseId;
        Name = name;
        Semester = semester;
        Credits = credits;
        Schedules = schedules;
    }

    // Opcional: Método para comparar dos instancias de CourseData
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var courseData = (CourseData)obj;
        return CourseID == courseData.CourseID &&
               Name == courseData.Name &&
               Semester == courseData.Semester &&
               Credits == courseData.Credits &&
               Schedules.SequenceEqual(courseData.Schedules);
    }

    // Opcional: Método para obtener el código hash de una instancia de CourseData
    public override int GetHashCode()
    {
        return HashCode.Combine(CourseID, Name, Semester, Credits, Schedules);
    }
}
using System.Collections.Generic;
namespace enrollments_microservice.Domain.ValueObjects;
public class ScheduleData
{
    public string Day { get; private set; }
    public string Group { get; private set; }
    public int Year { get; private set; }
    public string TeacherName { get; private set; }
    public List<IntervalData> Time { get; private set; }

    public ScheduleData(string day, string group, int year, string teacherName, List<IntervalData> time)
    {
        if (string.IsNullOrEmpty(day))
            throw new ArgumentException("Day must not be empty of ScheduleData");
        if (string.IsNullOrEmpty(group))
            throw new ArgumentException("Group must not be empty of ScheduleData");
        if (year <= 0)
            throw new ArgumentException("Year must be greater than 0 of ScheduleData");
        if (string.IsNullOrEmpty(teacherName))
            throw new ArgumentException("Teacher name must not be empty of ScheduleData");
        if (time == null || time.Count == 0)
            throw new ArgumentException("Time must not be empty of ScheduleData");
        Day = day;
        Group = group;
        Year = year;
        TeacherName = teacherName;
        Time = time;
    }

    // Opcional: Método para comparar dos instancias de ScheduleData
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var scheduleData = (ScheduleData)obj;
        return Day == scheduleData.Day &&
               Group == scheduleData.Group &&
               Year == scheduleData.Year &&
               TeacherName == scheduleData.TeacherName &&
               Time.SequenceEqual(scheduleData.Time);
    }

    // Opcional: Método para obtener el código hash de una instancia de ScheduleData
    public override int GetHashCode()
    {
        return HashCode.Combine(Day, Group, Year, TeacherName, Time);
    }
}
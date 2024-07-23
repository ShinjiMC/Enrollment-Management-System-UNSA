namespace enrollments_microservice.Domain.ValueObjects;

public class StudentData
{
    public int StudentID { get; private set; }
    public string FullName { get; private set; }
    public int AcademicPerformance { get; private set; }
    public int Credits { get; private set; }

    public StudentData(int studentId, string fullName, int academicPerformance, int credits)
    {
        if (studentId <= 0)
            throw new ArgumentException("Student ID must be greater than 0 of StudentData");
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException("Full name must not be empty of StudentData");
        if (academicPerformance < 0)
            throw new ArgumentException("Academic performance must be greater than or equal to 0 of StudentData");
        if (credits < 0)
            throw new ArgumentException("Credits must be greater than or equal to 0 of StudentData");
        StudentID = studentId;
        FullName = fullName;
        AcademicPerformance = academicPerformance;
        Credits = credits;
    }

    // Opcional: Método para comparar dos instancias de StudentData
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var studentData = (StudentData)obj;
        return StudentID == studentData.StudentID &&
               FullName == studentData.FullName &&
               AcademicPerformance == studentData.AcademicPerformance &&
               Credits == studentData.Credits;
    }

    // Opcional: Método para obtener el código hash de una instancia de StudentData
    public override int GetHashCode()
    {
        return HashCode.Combine(StudentID, FullName, AcademicPerformance, Credits);
    }
}
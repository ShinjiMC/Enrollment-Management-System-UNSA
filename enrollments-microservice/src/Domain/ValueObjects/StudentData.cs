namespace enrollments_microservice.Domain.ValueObjects;

public class StudentData
{
    public int StudentID { get; private set; }
    public string FullName { get; private set; }
    public int Credits { get; private set; }

    public StudentData(int studentId, string fullName, int credits)
    {
        StudentID = studentId;
        FullName = fullName;
        Credits = credits;
    }
}
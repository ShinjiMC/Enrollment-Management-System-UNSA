namespace enrollments_microservice.Domain.ValueObjects
public class EnrollData
{
    public int Id { get; private set; }

    public EnrollData(int id)
    {
        Id = id;
    }
}
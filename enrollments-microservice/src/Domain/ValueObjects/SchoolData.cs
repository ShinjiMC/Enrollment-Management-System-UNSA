namespace enrollments_microservice.Domain.ValueObjects;

public class SchoolData
{
    public string Name { get; private set; }

    public SchoolData(string name)
    {
        Name = name;
    }
}
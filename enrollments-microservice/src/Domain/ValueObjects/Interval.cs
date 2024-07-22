namespace enrollments_microservice.Domain.ValueObjects;
public class Interval
{
    public string Start { get; private set; }
    public string End { get; private set; }

    public Interval(string start, string end)
    {
        Start = start;
        End = end;
    }
}
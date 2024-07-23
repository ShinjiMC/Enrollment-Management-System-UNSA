namespace enrollments_microservice.Domain.ValueObjects;
public class IntervalData
{
    public string Start { get; private set; }
    public string End { get; private set; }

    public IntervalData(string start, string end)
    {
        if (string.IsNullOrEmpty(start))
            throw new ArgumentException("Start must not be empty of IntervalData");
        if (string.IsNullOrEmpty(end))
            throw new ArgumentException("End must not be empty of IntervalData");
        Start = start;
        End = end;
    }

    // Opcional: Método para comparar dos instancias de Interval
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var interval = (IntervalData)obj;
        return Start == interval.Start && End == interval.End;
    }

    // Opcional: Método para obtener el código hash de una instancia de Interval
    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
}
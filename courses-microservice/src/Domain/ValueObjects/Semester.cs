namespace Domain.Schedules
{
    public record Semester
    {
        internal Semester(string value) => Value = value;

        public string Value { get; init; }
    }
}

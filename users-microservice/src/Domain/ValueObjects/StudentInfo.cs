namespace users_microservice.Domain.ValueObjects
{
    public class StudentInfo
    {
        public string Cui { get; }
        public string? SchoolId { get; }

        public StudentInfo(string cui, string? schoolId = null)
        {

            if (string.IsNullOrEmpty(cui)) throw new ArgumentException("Cui cannot be null or empty");
            Cui = cui;
            SchoolId = schoolId;
        }

        // Opcional: Métodos para igualdad y hash
        public override bool Equals(object? obj)
        {
            if (obj is StudentInfo other)
            {
                return Cui == other.Cui && SchoolId == other.SchoolId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cui, SchoolId);
        }
    }
}

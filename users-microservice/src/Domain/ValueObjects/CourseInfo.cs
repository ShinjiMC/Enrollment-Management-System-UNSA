namespace users_microservice.Domain.ValueObjects
{
    public class CourseInfo
    {
        public string CourseId { get; }
        public string Status { get; }

        public CourseInfo(string courseId, string status)
        {
            if (string.IsNullOrEmpty(courseId)) 
                throw new ArgumentException("CourseId cannot be null or empty", nameof(courseId));
            if (string.IsNullOrEmpty(status)) 
                throw new ArgumentException("Status cannot be null or empty", nameof(status));

            CourseId = courseId;
            Status = status;
        }

        // Opcional: Método para comparar dos instancias de CourseInfo
        public override bool Equals(object? obj)
        {
            if (obj is CourseInfo other)
            {
                return CourseId == other.CourseId && Status == other.Status;
            }
            return false;
        }

        // Opcional: Método para obtener el código hash de CourseInfo
        public override int GetHashCode()
        {
            return HashCode.Combine(CourseId, Status);
        }
    }
}

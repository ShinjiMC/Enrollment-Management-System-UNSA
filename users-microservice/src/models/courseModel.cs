namespace users_microservice.models
{
    public class Course
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Semester { get; set; }
        public int Credits { get; set; }
    }
}
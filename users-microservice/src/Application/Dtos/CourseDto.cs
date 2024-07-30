namespace users_microservice.Application.Dtos;

public class CourseDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? CourseId { get; set; }
    public string? Status { get; set; }
    public DateTime? DateUpdated { get; set; }

}

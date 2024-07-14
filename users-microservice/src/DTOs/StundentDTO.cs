using System.ComponentModel.DataAnnotations;
namespace users_microservice.DTOs;

public class StudentDto
{
    public string? Id { get; set; } = string.Empty;
        
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string CUI { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    // You can add it if necessary, for example:
    // public List<int> CourseIds { get; set; } = new List<int>();
}
using System.ComponentModel.DataAnnotations;
using users_microservice.models;

namespace users_microservice.DTOs;
public class RegisterUserDto
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string School { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class RegisterAdminDto : RegisterUserDto
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}

public class RegisterStudentDto : RegisterUserDto
{
    [Required]
    public string CUI { get; set; } = string.Empty;

    public ICollection<CourseModel> Courses { get; set; } = new List<CourseModel>();
}
using System.ComponentModel.DataAnnotations;

namespace users_microservice.DTOs;

public class UserDto
{
    public string? Id { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;

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

    public string Role { get; set; } = string.Empty;
    
    public string phoneNumber { get; set; } = string.Empty;

}

using System.ComponentModel.DataAnnotations;
namespace users_microservice.Application.Dtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
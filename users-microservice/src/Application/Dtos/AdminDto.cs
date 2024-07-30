using System.ComponentModel.DataAnnotations;

namespace users_microservice.Application.Dtos;

public class AdminDto
{
    public string? Id { get; set; } = string.Empty;
    
    [Required]
    public string FullName { get; set; } = string.Empty;

   
    [Required]
    public UserDto? UserData { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; } = string.Empty;

}

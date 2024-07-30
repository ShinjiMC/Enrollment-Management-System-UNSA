using System.ComponentModel.DataAnnotations;

namespace users_microservice.Application.Dtos;

public class UserDto
{
    public string? Id { get; set; } = string.Empty;
    
    [Required]
    public string UserName { get; set; } = string.Empty;


    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
    
    

}

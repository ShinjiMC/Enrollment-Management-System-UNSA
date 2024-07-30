using Microsoft.AspNetCore.Identity;

namespace users_microservice.Domain.Entities;

public class UserModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}







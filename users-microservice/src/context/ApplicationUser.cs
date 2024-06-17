using Microsoft.AspNetCore.Identity;

namespace users_microservice.context;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    // public string? Pregunta { get; set; }
}
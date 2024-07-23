using Microsoft.AspNetCore.Identity;
using users_microservice.models;

namespace users_microservice.context;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}

public class Student : ApplicationUser
{
    public required string Cui { get; set; }
    public string? SchoolIds { get; set; }
    // Otros campos específicos del estudiante
}

public class Admin : ApplicationUser
{
    public string? phoneNumber { get; set; }  
}
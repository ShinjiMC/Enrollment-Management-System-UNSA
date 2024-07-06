using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace users_microservice.models;
public class UserModel : IdentityUser
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string School { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}

public class AdminUser : UserModel
{ public AdminUser()
    {
        Role = "Admin";
    }
}

public class StudentUser : UserModel
{
    [Required]
    public string CUI { get; set; } = string.Empty;

    public StudentUser()
    {
        Role = "Student";
    }

    public ICollection<CourseModel> Courses { get; set; } = new List<CourseModel>();
}
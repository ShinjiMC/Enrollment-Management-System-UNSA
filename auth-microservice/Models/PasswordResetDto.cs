namespace auth_microservice.Models
{
    public class PasswordResetDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}

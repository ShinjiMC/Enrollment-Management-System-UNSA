using auth_microservice.Models;

namespace auth_microservice.Services
{
    public interface IPasswordService
    {
        bool ResetPassword(PasswordResetDto passwordResetDto);
        bool ChangePassword(PasswordChangeDto passwordChangeDto);
    }
}

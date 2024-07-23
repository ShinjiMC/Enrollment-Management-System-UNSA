using auth_microservice.Models;

namespace auth_microservice.Repositories
{
    public interface IUserRepository
    {
        User ValidateUser(UserLoginDto userLoginDto);
        bool AddUser(UserRegisterDto userRegisterDto);
        bool ResetPassword(PasswordResetDto passwordResetDto);
        bool ChangePassword(PasswordChangeDto passwordChangeDto);
    }
}

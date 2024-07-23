using auth_microservice.Models;
using auth_microservice.Repositories;

namespace auth_microservice.Services
{
    public class PasswordServiceImpl : IPasswordService
    {
        private readonly IUserRepository _userRepository;

        public PasswordServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ResetPassword(PasswordResetDto passwordResetDto)
        {
            return _userRepository.ResetPassword(passwordResetDto);
        }

        public bool ChangePassword(PasswordChangeDto passwordChangeDto)
        {
            return _userRepository.ChangePassword(passwordChangeDto);
        }
    }
}

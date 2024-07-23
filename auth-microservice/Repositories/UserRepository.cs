using auth_microservice.Models;
using System.Collections.Generic;
using System.Linq;

namespace auth_microservice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>();

        public User ValidateUser(UserLoginDto userLoginDto)
        {
            return _users.FirstOrDefault(u => u.Username == userLoginDto.Username && u.Password == userLoginDto.Password);
        }

        public bool AddUser(UserRegisterDto userRegisterDto)
        {
            if (_users.Any(u => u.Username == userRegisterDto.Username))
            {
                return false;
            }

            _users.Add(new User
            {
                Id = _users.Count + 1,
                Username = userRegisterDto.Username,
                Password = userRegisterDto.Password,
                Role = "User"
            });

            return true;
        }

        public bool ResetPassword(PasswordResetDto passwordResetDto)
        {
            var user = _users.FirstOrDefault(u => u.Username == passwordResetDto.Email);
            if (user == null)
            {
                return false;
            }

            user.Password = passwordResetDto.NewPassword;
            return true;
        }

        public bool ChangePassword(PasswordChangeDto passwordChangeDto)
        {
            var user = _users.FirstOrDefault(u => u.Password == passwordChangeDto.OldPassword);
            if (user == null)
            {
                return false;
            }

            user.Password = passwordChangeDto.NewPassword;
            return true;
        }
    }
}

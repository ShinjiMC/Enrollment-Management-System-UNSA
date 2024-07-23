using auth_microservice.Models;
using auth_microservice.Repositories;

namespace auth_microservice.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerationService _tokenGenerationService;

        public AuthServiceImpl(IUserRepository userRepository, ITokenGenerationService tokenGenerationService)
        {
            _userRepository = userRepository;
            _tokenGenerationService = tokenGenerationService;
        }

        public Token Login(UserLoginDto userLoginDto)
        {
            var user = _userRepository.ValidateUser(userLoginDto);
            if (user == null)
                return null;

            return _tokenGenerationService.GenerateToken(user);
        }

        public bool Register(UserRegisterDto userRegisterDto)
        {
            return _userRepository.AddUser(userRegisterDto);
        }
    }
}

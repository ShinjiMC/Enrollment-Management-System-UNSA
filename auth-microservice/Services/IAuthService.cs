using auth_microservice.Models;

namespace auth_microservice.Services
{
    public interface IAuthService
    {
        Token Login(UserLoginDto userLoginDto);
        bool Register(UserRegisterDto userRegisterDto);
    }
}

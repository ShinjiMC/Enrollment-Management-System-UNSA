using auth_microservice.Models;

namespace auth_microservice.Services
{
    public interface ITokenGenerationService
    {
        Token GenerateToken(User user);
    }
}

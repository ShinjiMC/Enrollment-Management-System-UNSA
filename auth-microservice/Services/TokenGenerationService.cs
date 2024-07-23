using auth_microservice.Models;
using System;

namespace auth_microservice.Services
{
    public class TokenGenerationService : ITokenGenerationService
    {
        public Token GenerateToken(User user)
        {
            // Logic to generate a JWT token
            return new Token
            {
                AccessToken = "generated_token",
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}

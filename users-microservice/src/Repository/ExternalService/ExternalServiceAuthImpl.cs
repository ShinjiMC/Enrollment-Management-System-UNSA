
using users_microservice.Application.Dtos;


namespace users_microservice.Repository.ExternalService
{
    public class ExternalServiceAuthImpl : IExternalServiceAuth
    {
        private readonly HttpClient _httpClient;

        public ExternalServiceAuthImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterUserAsync(UserDto userDto)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:8002/api/register", userDto);
            return response.IsSuccessStatusCode;
        }
    }
}

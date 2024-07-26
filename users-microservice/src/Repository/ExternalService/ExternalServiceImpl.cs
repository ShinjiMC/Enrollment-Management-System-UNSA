
using users_microservice.Application.Dtos;
using users_microservice.Application.Service.Interface;


namespace users_microservice.Repository.ExternalService
{
    public class ExternalServiceImpl : IExternalService
    {
        private readonly HttpClient _httpClient;

        public ExternalServiceImpl(HttpClient httpClient)
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

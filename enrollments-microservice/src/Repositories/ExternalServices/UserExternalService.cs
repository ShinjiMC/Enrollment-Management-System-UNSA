using System.Text.Json;
using enrollments_microservice.Application.Dtos;

namespace enrollments_microservice.Repositories.ExternalServices
{
    public interface IUserExternalService
    {
        Task<UserExternalDto?> GetDataByUserIdAsync(string id);
    }

    public class UserExternalService : IUserExternalService
    {
        private readonly HttpClient _httpClient;

        public UserExternalService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            var baseUrl = configuration.GetSection("ExternalService:user").Value;
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("User API configuration is missing.");
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<UserExternalDto?> GetDataByUserIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"ExternalService/student/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    var user = JsonSerializer.Deserialize<UserExternalDto>(content);
                    return user;
                }
                catch (JsonException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
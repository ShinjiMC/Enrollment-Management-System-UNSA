using System.Text.Json;
using enrollments_microservice.Application.Dtos;

namespace enrollments_microservice.Repositories.ExternalServices;

public interface ISchoolExternalService
{
    Task<string?> GetNameBySchoolIdAsync(string id);
    Task<SchoolExternalDto?> GetCurriculumByIdAsync(string id);
}

public class SchoolExternalService : ISchoolExternalService
{
    private readonly HttpClient _httpClient;

    public SchoolExternalService(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        var baseUrl = configuration.GetSection("ExternalService:school").Value;
        if (string.IsNullOrEmpty(baseUrl))
            throw new InvalidOperationException("User API configuration is missing.");
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<string?> GetNameBySchoolIdAsync(string id)
    {
        /*var response = await _httpClient.GetAsync($"api/school/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var school = JsonSerializer.Deserialize<UserExternalDto>(content);
            return school ?? new SchoolExternalDto();
        }*/
        if (id == "1")
            return "Tokyo-1";
        return null;
    }

    public async Task<SchoolExternalDto?> GetCurriculumByIdAsync(string id)
    {
        /*var response = await _httpClient.GetAsync($"api/school/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var school = JsonSerializer.Deserialize<SchoolExternalDto>(content);
            return school ?? new SchoolExternalDto();
        }*/
        if (id == "1")
            return new SchoolExternalDto
            {
                FullName = "Tokyo-1",
                Curriculum = new List<PreCoursesExternalDto>
                {
                    new PreCoursesExternalDto
                    {
                        Id = "course1",
                        PreRequeriments = new List<string>()
                    },
                    new PreCoursesExternalDto
                    {
                        Id = "course2",
                        PreRequeriments = new List<string>()
                    },
                    new PreCoursesExternalDto
                    {
                        Id = "course3",
                        PreRequeriments = new List<string>{
                            "course1",
                            "course2"
                        }
                    },
                    new PreCoursesExternalDto
                    {
                        Id = "course4",
                        PreRequeriments = new List<string>()
                    }
                }
            };
        return null;
    }
}

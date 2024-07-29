using System.Text.Json;
using enrollments_microservice.Application.Dtos;

namespace enrollments_microservice.Repositories.ExternalServices;

public interface ICoursesExternalService
{
    Task<CourseExternalDto?> GetScheduleOfCourseByIdAsync(string id, string schoolId);
    Task<CourseExternalDto?> GetCreditsOfCourseByIdAsync(string id, string schoolId);
}

public class CoursesExternalService : ICoursesExternalService
{
    private readonly HttpClient _httpClient;

    public CoursesExternalService(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        var baseUrl = configuration.GetSection("ExternalService:course").Value;
        if (string.IsNullOrEmpty(baseUrl))
            throw new InvalidOperationException("Courses API configuration is missing.");
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<CourseExternalDto?> GetCreditsOfCourseByIdAsync(string id, string schoolId)
    {
        // by id and schoolid
        return id switch
        {
            "course1" => new CourseExternalDto
            {
                Credits = 4
            },
            "course2" => new CourseExternalDto
            {
                Credits = 4
            },
            "course3" => new CourseExternalDto
            {
                Credits = 30
            },
            "course4" => new CourseExternalDto
            {
                Credits = 30
            },
            _ => null
        };
    }

    public async Task<CourseExternalDto?> GetScheduleOfCourseByIdAsync(string id, string schoolId)
    {
        /*var response = await _httpClient.GetAsync($"api/courses/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<CourseExternalDto>(content);
            return course ?? new CourseExternalDto();
        }
        return new CourseExternalDto
        {
            Name = "Course not found",
            Schedule = new List<string>()
        };*/

        return id switch
        {
            "course3" => new CourseExternalDto
            {
                Name = "Russian",
                Semester = "A",
                Credits = 4,
                Schedules =
                [
                    new() {
                        Day = "Monday",
                        Group = "A",
                        Year = 2022,
                        TeacherName = "Asuka Langley",
                        Time =
                        [
                            new() {
                                DayOfWeek= "3",
                                Start = "08:00",
                                End = "10:30"
                            },
                            new() {
                                DayOfWeek= "3",
                                Start = "10:10",
                                End = "12:00"
                            }
                        ]
                    },
                    new() {
                        Day = "Wednesday",
                        Group = "B",
                        Year = 2022,
                        TeacherName = "Alya Roshidere",
                        Time =
                        [
                            new() {
                                DayOfWeek= "3",
                                Start = "08:20",
                                End = "10:40"
                            }
                        ]
                    }
                ]

            },
            "course4" => new CourseExternalDto
            {
                Name = "Evangelion",
                Semester = "C",
                Credits = 4,
                Schedules =
                [
                    new() {
                        Day = "Monday",
                        Group = "A",
                        Year = 2022,
                        TeacherName = "Asuka Langley",
                        Time =
                        [
                            new() {
                                DayOfWeek= "3",
                                Start = "08:00",
                                End = "10:00"
                            },
                            new() {
                                DayOfWeek= "3",
                                Start = "10:00",
                                End = "12:00"
                            }
                        ]
                    }
                ]
            },
            _ => null
        };
    }
}
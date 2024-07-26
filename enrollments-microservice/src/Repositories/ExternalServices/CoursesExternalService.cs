using System.Text.Json;
using enrollments_microservice.Application.Dtos;

namespace enrollments_microservice.Repositories.ExternalServices;

public interface ICoursesExternalService
{
    Task<CourseExternalDto?> GetScheduleOfCourseByIdAsync(string id);
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

    public async Task<CourseExternalDto?> GetScheduleOfCourseByIdAsync(string id)
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
                Schedules = new List<ScheduleExternalDto>
                {
                    new ScheduleExternalDto
                    {
                        Day = "Monday",
                        Group = "A",
                        Year = 2022,
                        TeacherName = "Asuka Langley",
                        Time = new List<IntervalExternalDto>
                        {
                            new IntervalExternalDto
                            {
                                Start = "08:00",
                                End = "10:00"
                            },
                            new IntervalExternalDto
                            {
                                Start = "10:00",
                                End = "12:00"
                            }
                        }
                    },
                    new ScheduleExternalDto
                    {
                        Day = "Wednesday",
                        Group = "B",
                        Year = 2022,
                        TeacherName = "Alya Roshidere",
                        Time = new List<IntervalExternalDto>
                        {
                            new IntervalExternalDto
                            {
                                Start = "08:00",
                                End = "10:00"
                            }
                        }
                    }
                }

            },
            "course4" => new CourseExternalDto
            {
                Name = "Evangelion",
                Semester = "C",
                Credits = 4,
                Schedules = new List<ScheduleExternalDto>
                {
                    new ScheduleExternalDto
                    {
                        Day = "Monday",
                        Group = "A",
                        Year = 2022,
                        TeacherName = "Asuka Langley",
                        Time = new List<IntervalExternalDto>
                        {
                            new IntervalExternalDto
                            {
                                Start = "08:00",
                                End = "10:00"
                            },
                            new IntervalExternalDto
                            {
                                Start = "10:00",
                                End = "12:00"
                            }
                        }
                    }
                }
            },
            _ => throw new NotImplementedException()
        };
    }
}
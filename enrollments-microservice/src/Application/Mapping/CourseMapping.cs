using enrollments_microservice.Application.Dtos;
using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;

namespace enrollments_microservice.Application.Mapping;

public static class CourseMapping
{
    public static CourseDto ToDto(this CourseModel courseModel)
    {
        return new CourseDto
        {
            Id = courseModel.Id.ToString(),
            Group = courseModel.Group
        };
    }

    public static CourseModel ToModel(this CourseDto courseDto)
    {
        return new CourseModel
        {
            Id = int.TryParse(courseDto.Id, out var id) ? id : 0,
            CourseId = int.TryParse(courseDto.Id, out var courseId) ? courseId : 0,
            Group = courseDto.Group
        };
    }
}
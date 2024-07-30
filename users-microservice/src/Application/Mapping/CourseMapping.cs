using users_microservice.Domain.Entities;
using users_microservice.Application.Dtos;
using users_microservice.Domain.ValueObjects;

namespace users_microservice.Application.Mapping
{
    public static class CourseMapping
    {
        public static CourseDto ToDto(this CourseModel courseModel)
        {
            return new CourseDto
            {
                Id = courseModel.Id,
                StudentId = courseModel.StudentId,
                CourseId = courseModel.CourseData?.CourseId,
                Status = courseModel.CourseData?.Status,
                DateUpdated = courseModel.DateUpdated
            };
        }

        public static CourseModel ToModel(this CourseDto courseDto)
        {
            return new CourseModel
            {
                Id = courseDto.Id,
                StudentId = courseDto.StudentId,
                CourseData = (courseDto.CourseId != null && courseDto.Status != null)
                    ? new CourseInfo(courseDto.CourseId, courseDto.Status)
                    : null,
                DateUpdated = courseDto.DateUpdated
            };
        }

        public static CourseModel ToModel(string courseId, int studentId)
        {
            return new CourseModel
            {
                StudentId = studentId,
                CourseData = new CourseInfo(courseId, "Status Inicial"),
                DateUpdated = DateTime.UtcNow
            };
        }
    }
}

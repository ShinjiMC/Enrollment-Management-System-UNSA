using MediatR;
using ErrorOr;
using Courses.Common;

namespace Application.Courses.Create
{
    public record CreateCourseCommand(
        string Name,
        int Credits,
        int Hours,
        bool Active,
        string Semester,  
        string SchoolId    
    ) : IRequest<ErrorOr<CourseResponse>>;
}

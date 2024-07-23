using MediatR;
using ErrorOr;

namespace Application.Courses.Create
{
    public record CreateCourseCommand(
        string Name,
        int Credits,
        int Hours,
        bool Active,
        string Semester,  
        int SchoolId    
    ) : IRequest<ErrorOr<Unit>>;
}

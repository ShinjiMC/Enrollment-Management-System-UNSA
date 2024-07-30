using ErrorOr;
using MediatR;

namespace Application.Courses.Update;

public record UpdateCourseCommand(
    Guid Id,
    string Name,
    int Credits,
    int Hours,
    bool Active,
    string Semester,
    string SchoolId
) : IRequest<ErrorOr<Unit>>;
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
    int SchoolId
) : IRequest<ErrorOr<Unit>>;
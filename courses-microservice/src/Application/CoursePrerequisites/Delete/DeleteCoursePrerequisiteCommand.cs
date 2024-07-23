using MediatR;
using ErrorOr;
namespace Application.CoursesPrerequisite.Delete;

public record DeleteCoursePrerequisiteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
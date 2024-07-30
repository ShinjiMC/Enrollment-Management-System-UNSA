using MediatR;
using ErrorOr;
namespace Application.Courses.Delete;

public record DeleteCourseCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
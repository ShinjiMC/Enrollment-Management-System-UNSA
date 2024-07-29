using MediatR;
using ErrorOr;
namespace Application.CoursesPrerequisite.Delete;

public record DeleteCoursePrerequisiteCommand(Guid CourseId, Guid CoursePrerequisiteId) : IRequest<ErrorOr<Unit>>;
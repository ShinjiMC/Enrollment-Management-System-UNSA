using MediatR;
using ErrorOr;
using Domain.Courses;

namespace Application.CoursesPrerequisite.Create
{
    public record CreateCoursesPrerequisiteCommand(
        string CourseId,
        string CoursePrerequisiteId
    ) : IRequest<ErrorOr<Unit>>;
}

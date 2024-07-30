using MediatR;
using ErrorOr;
using Domain.Courses;
using CoursesPrerequisite.Common;

namespace Application.CoursesPrerequisite.Create
{
    public record CreateCoursesPrerequisiteCommand(
        string CourseId,
        string CoursePrerequisiteId
    ) : IRequest<ErrorOr<CoursesPrerequisiteResponse>>;
}

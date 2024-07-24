using MediatR;
using ErrorOr;
using CoursesPrerequisite.Common;
namespace Application.CoursesPrerequisite.GetAll;

public record GetAllCoursesPrerequisiteQuery(string Id) : IRequest<ErrorOr<IReadOnlyList<CoursesPrerequisiteResponseGetAll>>>;
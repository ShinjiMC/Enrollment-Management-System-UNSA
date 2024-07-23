using Domain.Courses;
using MediatR;
using ErrorOr;
using Courses.Common;
namespace Application.Courses.GetAll;

internal sealed class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, ErrorOr<IReadOnlyList<CourseResponse>>>
{
    private readonly ICourseRepository _CourseRepository;
    public GetAllCoursesQueryHandler(ICourseRepository CourseRepository)
    {
        _CourseRepository = CourseRepository ?? throw new ArgumentNullException(nameof(CourseRepository));
    }
    public async Task<ErrorOr<IReadOnlyList<CourseResponse>>> Handle(GetAllCoursesQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Course> Courses = await _CourseRepository.GetAll();

        return Courses.Select(Course => new CourseResponse(
                Course.CourseId,
                Course.Name,
                Course.Credits,
                Course.Hours,
                Course.Active,
                Course.Semester.Value,
                Course.SchoolId
        )).ToList();
    }
}
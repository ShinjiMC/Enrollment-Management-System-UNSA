using Application.CoursesPrerequisite.GetAll;
using Courses.Common;
using CoursesPrerequisite.Common;
using Domain.Courses;
using ErrorOr;
using MediatR;

namespace Application.Courses.GetAll;


internal sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCoursesPrerequisiteQuery, ErrorOr<IReadOnlyList<CoursesPrerequisiteResponseGetAll>>>
{
    private readonly ICoursePrerequisiteRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICoursePrerequisiteRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<CoursesPrerequisiteResponseGetAll>>> Handle(GetAllCoursesPrerequisiteQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<CoursePrerequisite> courses = await _customerRepository.GetPrerequisitesByCourseIdAsync(new Guid(query.Id));

        return courses.Select(courses => new CoursesPrerequisiteResponseGetAll(
                courses.CourseId.ToString(),
                courses.PrerequisiteCourseId.ToString()
            )).ToList();
    }
}
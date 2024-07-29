using Domain.Courses;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;
using CoursesPrerequisite.Common;

namespace Application.CoursesPrerequisite.Create
{
    internal sealed class CreateCoursesPrerequisiteCommandHandler : IRequestHandler<CreateCoursesPrerequisiteCommand, ErrorOr<CoursesPrerequisiteResponse>>
    {
        private readonly ICoursePrerequisiteRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCoursesPrerequisiteCommandHandler(
            ICoursePrerequisiteRepository courseRepository,
            IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<CoursesPrerequisiteResponse>> Handle(CreateCoursesPrerequisiteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var course = new CoursePrerequisite(
                    new Guid(command.CourseId),
                    new Guid(command.CoursePrerequisiteId)
                );
                _courseRepository.Add(course);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = new CoursesPrerequisiteResponse(course.CourseId, course.PrerequisiteCourseId);
                return response;
            }
            catch (Exception ex)
            {
                return Error.Failure("Course.Create.Failure", ex.Message);
            }
        }
    }
}

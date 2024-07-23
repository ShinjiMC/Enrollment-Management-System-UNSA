using Domain.Courses;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;

namespace Application.CoursesPrerequisite.Create
{
    internal sealed class CreateCoursesPrerequisiteCommandHandler : IRequestHandler<CreateCoursesPrerequisiteCommand, ErrorOr<Unit>>
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

        public async Task<ErrorOr<Unit>> Handle(CreateCoursesPrerequisiteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var course = new CoursePrerequisite(
                    new CourseId(new Guid(command.CourseId)),
                    new CourseId(new Guid(command.CoursePrerequisiteId))
                );
                _courseRepository.Add(course);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("Course.Create.Failure", ex.Message);
            }
        }
    }
}

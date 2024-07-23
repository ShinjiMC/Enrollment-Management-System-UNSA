using Domain.Courses;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;

namespace Application.Courses.Create
{
    internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Unit>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(
            ICourseRepository courseRepository,
            IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Validar semestre
                if (Semester.Create(command.Semester) is not Semester semester)
                {
                    return Errors.Course.InvalidSemester;
                }

                var course = new Course(
                    new CourseId(Guid.NewGuid()),  // Generar un nuevo ID para el curso
                    command.Name,
                    command.Credits,
                    command.Hours,
                    command.Active,
                    semester,
                    command.SchoolId
                );

                // Agregar el curso al repositorio
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

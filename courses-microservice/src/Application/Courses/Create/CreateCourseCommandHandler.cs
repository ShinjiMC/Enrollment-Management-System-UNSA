using Domain.Courses;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;
using Domain.Schedules;
using Courses.Common;

namespace Application.Courses.Create
{
    internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<CourseResponse>>
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

        public async Task<ErrorOr<CourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Validar semestre
                if (SemesterFactory.Create(command.Semester) is not Semester semester)
                {
                    return Errors.Course.InvalidSemester;
                }

                var course = new Course(
                    Guid.NewGuid(),  // Generar un nuevo ID para el curso
                    command.Name,
                    command.Credits,
                    command.Hours,
                    command.Active,
                    semester,
                    command.SchoolId
                );

                if(command.Hours == 0){
                    return Errors.Course.InvalidHours;
                }
                if(command.Credits == 0){
                    return Errors.Course.InvalidCredits;
                }

                // Agregar el curso al repositorio
                _courseRepository.Add(course);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = new CourseResponse(course.CourseId, course.Name, course.Credits, course.Hours, course.Active, course.Semester.Value, course.SchoolId);
                return response;
            }
            catch (Exception ex)
            {
                return Error.Failure("Course.Create.Failure", ex.Message);
            }
        }
    }
}

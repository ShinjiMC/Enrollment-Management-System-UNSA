using MediatR;
using ErrorOr;
using Application.Schedules.Common;

namespace Application.Schedules.GetByYearSemesterSchool
{
    public class GetByYearSemesterSchool : IRequest<ErrorOr<List<ScheduleResponse>>>
    {
        public int Year { get; }
        public string Semester { get; }
        public string SchoolId { get; }

        public GetByYearSemesterSchool(int year, string semester, string schoolId)
        {
            Year = year;
            Semester = semester;
            SchoolId = schoolId;
        }
    }
}

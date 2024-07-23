using Domain.Courses;
using Domain.Schedules.ValueObjects;

namespace Domain.Schedules.Services.Interfaces
{
    public interface IScheduleDomainService
    {
        bool ValidateScheduleEntries(IEnumerable<ScheduleEntry> entries, Course course);
    }
}

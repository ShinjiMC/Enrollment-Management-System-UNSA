using Domain.Schedules.ValueObjects;

namespace Domain.Schedules
{
    public static class ScheduleEntryFactory
    {
        private static readonly TimeSpan MinTime = TimeSpan.Zero;
        private static readonly TimeSpan MaxTime = new TimeSpan(23, 59, 59);

        public static ScheduleEntry? Create(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime < MinTime || startTime > MaxTime || endTime < MinTime || endTime > MaxTime || endTime <= startTime)
            {
                return null;
            }

            return new ScheduleEntry(dayOfWeek, startTime, endTime);
        }
    }
}

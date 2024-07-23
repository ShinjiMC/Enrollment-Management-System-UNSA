
namespace Domain.Schedules
{
    public partial record ScheduleEntry
    {
        private static readonly TimeSpan MinTime = TimeSpan.Zero;
        private static readonly TimeSpan MaxTime = new TimeSpan(23, 59, 59);

        public ScheduleEntry(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public DayOfWeek DayOfWeek { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }

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

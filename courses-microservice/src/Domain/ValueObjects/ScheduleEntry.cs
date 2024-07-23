namespace Domain.Schedules.ValueObjects
{
    public partial record ScheduleEntry
    {
        private static readonly TimeSpan MinTime = TimeSpan.Zero;
        private static readonly TimeSpan MaxTime = new TimeSpan(23, 59, 59);

        internal ScheduleEntry(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public DayOfWeek DayOfWeek { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
    }
}

namespace Domain.Schedules.ValueObjects
{
    public partial record ScheduleEntry
    {
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

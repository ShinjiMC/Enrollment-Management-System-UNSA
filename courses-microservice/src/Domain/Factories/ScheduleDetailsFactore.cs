namespace Domain.Schedules
{
    public static class ScheduleDetailsFactory
    {
        public static ScheduleDetails? Create(string group, string professorId)
        {
            if (string.IsNullOrEmpty(group) || string.IsNullOrEmpty(professorId))
            {
                return null;
            }

            return new ScheduleDetails(group, professorId);
        }
    }
}

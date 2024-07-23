namespace Domain.Schedules
{
    public class ScheduleDetails
    {
        public string Group { get; private set; }
        public string ProfessorId { get; private set; }

        private ScheduleDetails(string group, string professorId)
        {
            Group = group;
            ProfessorId = professorId;
        }

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

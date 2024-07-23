namespace Domain.Schedules
{
    public class ScheduleDetails
    {
        public string Group { get; private set; }
        public string ProfessorId { get; private set; }

        internal ScheduleDetails(string group, string professorId)
        {
            Group = group;
            ProfessorId = professorId;
        }
    }
}

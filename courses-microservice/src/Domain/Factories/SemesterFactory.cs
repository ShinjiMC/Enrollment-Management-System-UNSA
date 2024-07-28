using System.Text.RegularExpressions;

namespace Domain.Schedules
{
    public static class SemesterFactory
    {
        private const string Pattern = @"^[AB]$"; // Solo permite "A" o "B"

        public static Semester? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !SemesterRegex().IsMatch(value))
            {
                return null;
            }

            return new Semester(value);
        }

        private static Regex SemesterRegex() => new Regex(Pattern, RegexOptions.Compiled);
    }
}

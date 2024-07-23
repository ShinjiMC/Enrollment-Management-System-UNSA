using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Semester
    {
        private const string Pattern = @"^[AB]$"; // Solo permite "A" o "B"

        private Semester(string value) => Value = value;

        public static Semester? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !SemesterRegex().IsMatch(value))
            {
                return null;
            }

            return new Semester(value);
        }

        public string Value { get; init; }

        [GeneratedRegex(Pattern)]
        private static partial Regex SemesterRegex();
    }
}

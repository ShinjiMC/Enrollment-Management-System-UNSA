using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class Errors
    {
        public static class Course
        {
            public static Error InvalidCourseName => 
                Error.Validation("Course.Name", "Course name is invalid.");

            public static Error InvalidCredits => 
                Error.Validation("Course.Credits", "Credits must be a positive integer.");

            public static Error InvalidHours => 
                Error.Validation("Course.Hours", "Hours must be a positive integer.");

            public static Error InvalidSemester => 
                Error.Validation("Course.Semester", "Semester must be either 'A' or 'B'.");

            public static Error SchoolNotFound => 
                Error.Validation("Course.School", "School does not exist or is not valid.");
        }
    }
}

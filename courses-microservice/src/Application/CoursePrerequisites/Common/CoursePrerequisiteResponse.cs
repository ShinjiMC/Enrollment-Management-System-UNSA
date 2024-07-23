using Domain.Courses;

namespace CoursesPrerequisite.Common;

public record CoursesPrerequisiteResponse(
    CourseId CourseId,
    CourseId CoursePrerequisiteId
);

public record CoursesPrerequisiteResponseGetAll(
    string CourseId,
    string CoursePrerequisiteId
);


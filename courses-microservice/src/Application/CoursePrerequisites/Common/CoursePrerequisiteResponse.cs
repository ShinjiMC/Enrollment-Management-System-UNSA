using Domain.Courses;

namespace CoursesPrerequisite.Common;

public record CoursesPrerequisiteResponse(
    Guid CourseId,
    Guid CoursePrerequisiteId
);

public record CoursesPrerequisiteResponseGetAll(
    string CourseId,
    string CoursePrerequisiteId
);


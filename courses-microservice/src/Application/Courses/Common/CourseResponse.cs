namespace Courses.Common;

public record CourseResponse(
    Guid CourseId,
    string Name,
    int Credits,
    int Hours,
    bool Active,
    string Semester,
    int SchoolId
);
namespace SchoolsMicroservice.Models;
public class StudyPlanCourse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CoursePrerequisite> Prerequisites { get; set; }
}

public class CoursePrerequisite
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int PrerequisiteCourseId { get; set; }
}

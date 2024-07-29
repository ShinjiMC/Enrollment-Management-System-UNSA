namespace SchoolsMicroservice.Models;

public class StudyPlanSchool
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CourseOfStudyPlan> CoursesOfStudyPlan { get; set; }
}

public class CourseOfStudyPlan
{
    public int IdCourse { get; set; }
    public List<int> PrerequisitesCourse { get; set; }
}

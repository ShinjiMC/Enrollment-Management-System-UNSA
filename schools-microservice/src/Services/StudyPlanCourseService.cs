using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public class StudyPlanCourseService : IStudyPlanCourseService
{
    private readonly IStudyPlanCourseRepository _studyPlanCourseRepository;

    public StudyPlanCourseService(IStudyPlanCourseRepository studyPlanCourseRepository)
    {
        _studyPlanCourseRepository = studyPlanCourseRepository;
    }

    public StudyPlanCourse GetCourseWithPrerequisites(int courseId)
    {
        return _studyPlanCourseRepository.GetCourseWithPrerequisites(courseId);
    }
    public List<StudyPlanCourse> GetAllCoursesWithPrerequisites()
    {
        return _studyPlanCourseRepository.GetAllCoursesWithPrerequisites();
    }

    public void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
    {
        _studyPlanCourseRepository.AddStudyPlanCourse(studyPlanCourse);
    }
}
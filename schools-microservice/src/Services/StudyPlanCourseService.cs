using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public class StudyPlanCourseService : IStudyPlanCourseService
{
    private readonly IStudyPlanCourseRepository _studyPlanCourseRepository;

    public StudyPlanCourseService(IStudyPlanCourseRepository studyPlanCourseRepository)
    {
        _studyPlanCourseRepository = studyPlanCourseRepository;
    }

    public StudyPlanCourse GetStudyPlanById(int courseId)
    {
        return _studyPlanCourseRepository.GetStudyPlanById(courseId);
    }
    public List<StudyPlanCourse> GetAllStudyPlans()
    {
        return _studyPlanCourseRepository.GetAllStudyPlans();
    }

    public void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
    {
        _studyPlanCourseRepository.AddStudyPlanCourse(studyPlanCourse);
    }
}
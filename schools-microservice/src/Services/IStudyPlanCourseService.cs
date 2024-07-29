using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IStudyPlanCourseService
{
    StudyPlanCourse GetStudyPlanById(int courseId);
    List<StudyPlanCourse> GetAllStudyPlans();

    void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse);
}
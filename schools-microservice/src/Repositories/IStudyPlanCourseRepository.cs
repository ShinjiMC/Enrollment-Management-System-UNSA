using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IStudyPlanCourseRepository
{
    StudyPlanCourse GetStudyPlanById(int courseId);
    List<StudyPlanCourse> GetAllStudyPlans(); // Nuevo método
    void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse);
}

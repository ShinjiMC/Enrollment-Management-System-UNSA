using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IStudyPlanCourseRepository
{
    StudyPlanCourse GetCourseWithPrerequisites(int courseId);
    List<StudyPlanCourse> GetAllCoursesWithPrerequisites(); // Nuevo método
    void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse);
}

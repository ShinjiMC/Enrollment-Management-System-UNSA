using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IStudyPlanCourseService
{
    StudyPlanCourse GetCourseWithPrerequisites(int courseId);
    List<StudyPlanCourse> GetAllCoursesWithPrerequisites();

    void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse);
}
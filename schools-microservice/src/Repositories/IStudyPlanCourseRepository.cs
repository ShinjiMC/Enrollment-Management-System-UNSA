using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface IStudyPlanSchoolRepository
{
    StudyPlanSchool GetStudyPlanBySchoolName(string schoolName);
    StudyPlanSchool GetStudyPlanBySchoolId(int schoolId);
    void AddStudyPlanSchool(StudyPlanSchool studyPlanSchool);
}
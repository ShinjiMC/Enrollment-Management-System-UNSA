using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public class StudyPlanSchoolService : IStudyPlanSchoolService
{
    private readonly IStudyPlanSchoolRepository _studyPlanSchoolRepository;

    public StudyPlanSchoolService(IStudyPlanSchoolRepository studyPlanSchoolRepository)
    {
        _studyPlanSchoolRepository = studyPlanSchoolRepository;
    }

    public StudyPlanSchool GetStudyPlanBySchoolName(string schoolName)
    {
        return _studyPlanSchoolRepository.GetStudyPlanBySchoolName(schoolName);
    }

    public StudyPlanSchool GetStudyPlanBySchoolId(int schoolId)
    {
        return _studyPlanSchoolRepository.GetStudyPlanBySchoolId(schoolId);
    }

    public void AddStudyPlanSchool(StudyPlanSchool studyPlanSchool)
    {
        _studyPlanSchoolRepository.AddStudyPlanSchool(studyPlanSchool);
    }
}

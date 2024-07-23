namespace Domain.Courses
{
    public interface ICoursePrerequisiteRepository
    {
        Task<List<CoursePrerequisite>> GetPrerequisitesByCourseIdAsync(CourseId courseId);
        void Add(CoursePrerequisite course);
        void Update(CoursePrerequisite course);
        void Delete(CoursePrerequisite course);
        Task<CoursePrerequisite> GetByIdAsync(CourseId courseId);
    }
}
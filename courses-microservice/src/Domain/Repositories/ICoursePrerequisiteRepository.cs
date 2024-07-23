namespace Domain.Courses
{
    public interface ICoursePrerequisiteRepository
    {
        Task<List<CoursePrerequisite>> GetPrerequisitesByCourseIdAsync(Guid courseId);
        void Add(CoursePrerequisite course);
        void Update(CoursePrerequisite course);
        void Delete(CoursePrerequisite course);
        Task<CoursePrerequisite?> GetByIdAsync(Guid courseId);
    }
}
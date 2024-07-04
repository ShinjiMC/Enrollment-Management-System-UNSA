using course_microservice.DTOs;
using course_microservice.models;
using course_microservice.repositories;

namespace course_microservice.services
{
    public interface ICourseService 
    {
        Task<List<CourseModel>> GetAllCourses();
        Task<CourseModel> GetCourse(int ID);
        Task<CourseModel> AddCourse(CourseModel course);
        Task<CourseModel> UpdateCourse(int ID, CourseModel course);
        Task<bool> DeleteCourse(int ID);
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Task<List<CourseModel>> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }

        public Task<CourseModel> GetCourse(int ID)
        {
            return _courseRepository.GetCourse(ID);
        }

        public Task<CourseModel> AddCourse(CourseModel course)
        {
            return _courseRepository.AddCourse(course);
        }

        public Task<CourseModel> UpdateCourse(int ID, CourseModel course)
        {
            return _courseRepository.UpdateCourse(ID, course);
        }

        public Task<bool> DeleteCourse(int ID)
        {
            return _courseRepository.DeleteCourse(ID);
        }
    }
}

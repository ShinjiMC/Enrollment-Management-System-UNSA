using course_microservice.DTOs;
using course_microservice.models;
using course_microservice.repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace course_microservice.services
{
    public interface ISchoolService
    {
        Task<List<SchoolModel>> GetAllSchools();
        Task<SchoolModel> GetSchool(int ID);
        Task<SchoolModel> AddSchool(SchoolModel school);
        Task<SchoolModel> UpdateSchool(int ID, SchoolModel school);
        Task<bool> DeleteSchool(int ID);
    }

    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public Task<List<SchoolModel>> GetAllSchools()
        {
            return _schoolRepository.GetAllSchools();
        }

        public Task<SchoolModel> GetSchool(int ID)
        {
            return _schoolRepository.GetSchool(ID);
        }

        public Task<SchoolModel> AddSchool(SchoolModel school)
        {
            return _schoolRepository.AddSchool(school);
        }

        public Task<SchoolModel> UpdateSchool(int ID, SchoolModel school)
        {
            return _schoolRepository.UpdateSchool(ID, school);
        }

        public Task<bool> DeleteSchool(int ID)
        {
            return _schoolRepository.DeleteSchool(ID);
        }
    }
}

using course_microservice.context;
using course_microservice.DTOs;
using course_microservice.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace course_microservice.repositories
{
    public interface ISchoolRepository
    {
        Task<List<SchoolModel>> GetAllSchools();
        Task<SchoolModel> GetSchool(int ID);
        Task<SchoolModel> AddSchool(SchoolModel school);
        Task<SchoolModel> UpdateSchool(int ID, SchoolModel updatedSchool);
        Task<bool> DeleteSchool(int ID);
    }

    public class SchoolRepository : ISchoolRepository
    {
        private readonly MyDbContext _dbContext;

        public SchoolRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SchoolModel>> GetAllSchools()
        {
            return await _dbContext.School.ToListAsync();
        }

        public async Task<SchoolModel> GetSchool(int ID)
        {
            return await _dbContext.School.FindAsync(ID);
        }

        public async Task<SchoolModel> AddSchool(SchoolModel school)
        {
            // Verificar si ya existe una escuela con el mismo ID
            var existingSchool = await _dbContext.School.FindAsync(school.SchoolID);
            if (existingSchool != null)
            {
                // Si ya existe una escuela con el mismo ID, no la agregamos y devolvemos null
                return null;
            }

            // Si no hay una escuela con el mismo ID, la agregamos a la base de datos
            _dbContext.School.Add(school);
            await _dbContext.SaveChangesAsync();
            return school;
        }

        public async Task<SchoolModel> UpdateSchool(int ID, SchoolModel updatedSchool)
        {
            var school = await _dbContext.School.FindAsync(ID);
            if (school != null)
            {
                school.Name = updatedSchool.Name;
                school.Faculty = updatedSchool.Faculty;
                school.Area = updatedSchool.Area;
                school.FoundationDate = updatedSchool.FoundationDate;
                await _dbContext.SaveChangesAsync();
            }
            return school;
        }

        public async Task<bool> DeleteSchool(int ID)
        {
            var school = await _dbContext.School.FindAsync(ID);
            if (school != null)
            {
                _dbContext.School.Remove(school);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

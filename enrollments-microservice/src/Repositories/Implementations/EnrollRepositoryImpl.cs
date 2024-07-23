using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Repositories.Data;
using enrollments_microservice.Domain.Repositories;
using static enrollments_microservice.Application.Dtos.ServiceResponses;
using MongoDB.Driver;
namespace enrollments_microservice.Repositories.Implementations;

public class EnrollRepository : IEnrollRepository
{
    private readonly MongoDbContext _context;

    public EnrollRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<GeneralResponse> CreateEnroll(EnrollModel enrollModel)
    {
        try
        {
            var nextId = _context.GetNextSequenceValue("enroll_model_id");
            enrollModel.Id = nextId;
            await _context.EnrollModel.InsertOneAsync(enrollModel);
            return new GeneralResponse(true, "Enroll created successfully", 201);
        }
        catch (Exception ex)
        {
            return new GeneralResponse(false, ex.Message, 403);
        }
    }

    public async Task<EnrollModel> GetEnrollById(int id)
    {
        return await _context.EnrollModel.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<EnrollModel>> GetEnrollsByUserId(int userId)
    {
        var filter = Builders<EnrollModel>.Filter.Eq(enroll => enroll.Student.StudentID, userId);
        return await _context.EnrollModel.Find(filter).ToListAsync();
    }

    public async Task<EnrollModel> GetEnrollByUserIdAndSchoolId(int userId, int schoolId)
    {
        var filter = Builders<EnrollModel>.Filter.And(
            Builders<EnrollModel>.Filter.Exists("Student"),
            Builders<EnrollModel>.Filter.Eq("Student.StudentID", userId),
            Builders<EnrollModel>.Filter.Eq("SchoolID", schoolId)
        );
        return await _context.EnrollModel.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<EnrollModel>> GetEnrolls()
    {
        return await _context.EnrollModel.Find(_ => true).ToListAsync();
    }

    public async Task<List<EnrollModel>> GetEnrollsBySchoolId(int schoolId)
    {
        var filter = Builders<EnrollModel>.Filter.And(
            Builders<EnrollModel>.Filter.Exists("School"),
            Builders<EnrollModel>.Filter.Eq("School.SchoolID", schoolId)
        );
        return await _context.EnrollModel.Find(filter).ToListAsync();
    }

    public async Task<GeneralResponse> UpdateEnroll(EnrollModel enrollModel)
    {
        try
        {
            await _context.EnrollModel.ReplaceOneAsync(x => x.Id == enrollModel.Id, enrollModel);
            return new GeneralResponse(true, "Enroll updated successfully", 200);
        }
        catch (Exception ex)
        {
            return new GeneralResponse(false, ex.Message, 403);
        }
    }

    public async Task<GeneralResponse> DeleteEnroll(int id)
    {
        try
        {
            await _context.EnrollModel.DeleteOneAsync(x => x.Id == id);
            return new GeneralResponse(true, "Enroll deleted successfully", 200);
        }
        catch (Exception ex)
        {
            return new GeneralResponse(false, ex.Message, 403);
        }
    }
}
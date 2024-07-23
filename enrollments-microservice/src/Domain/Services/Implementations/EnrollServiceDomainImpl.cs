namespace enrollments_microservice.Domain.Services.Implementations;
using enrollments_microservice.Domain.Services.Interfaces;
using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.Repositories;
using enrollments_microservice.Domain.ValueObjects;
using System.Collections.Generic;
using static enrollments_microservice.Application.Dtos.ServiceResponses;

public class EnrollServiceDomain : IEnrollServiceDomain
{
    private readonly IEnrollRepository _enrollRepository;

    public EnrollServiceDomain(IEnrollRepository enrollRepository)
    {
        _enrollRepository = enrollRepository;
    }

    public async Task<GeneralResponse> CreateEnroll(EnrollModel enrollModel)
    {
        if (enrollModel == null)
            return new GeneralResponse(false, "Model is Empty", 400);
        if (enrollModel.Student == null)
            return new GeneralResponse(false, "Student is required", 400);
        if (enrollModel.School == null)
            return new GeneralResponse(false, "School is required", 400);

        var createEnrollResult = await _enrollRepository.CreateEnroll(enrollModel);
        if (!createEnrollResult.Flag)
            return new GeneralResponse(false, createEnrollResult.Message, 403);
        return new GeneralResponse(true, "Enroll created successfully", 201);
    }

    public async Task<EnrollModel?> GetEnrollById(int id)
    {
        var enroll = await _enrollRepository.GetEnrollById(id);
        if (enroll == null)
            return null;
        return enroll;
    }

    public async Task<EnrollModel?> GetEnrollByUserId(int userId)
    {
        var enroll = await _enrollRepository.GetEnrollByUserId(userId);
        if (enroll == null)
            return null;
        return enroll;
    }

    public async Task<EnrollModel?> GetEnrollByUserIdAndSchoolId(int userId, int schoolId)
    {
        var enroll = await _enrollRepository.GetEnrollByUserIdAndSchoolId(userId, schoolId);
        if (enroll == null)
            return null;
        return enroll;
    }

    public async Task<List<EnrollModel>?> GetEnrollsBySchoolId(int schoolId)
    {
        var enrolls = await _enrollRepository.GetEnrollsBySchoolId(schoolId);
        if (enrolls == null)
            return null;
        return enrolls;
    }

    public async Task<List<EnrollModel>?> GetEnrolls()
    {
        var enrolls = await _enrollRepository.GetEnrolls();
        if (enrolls == null)
            return null;
        return enrolls;
    }

    public async Task<GeneralResponse> UpdateEnroll(EnrollModel enrollModel)
    {
        string enrollId = enrollModel.Id.ToString();
        if (string.IsNullOrEmpty(enrollId))
            return new GeneralResponse(false, "Enroll Id is required", 400);
        var enroll = await _enrollRepository.GetEnrollById(enrollModel.Id);
        if (enroll == null)
            return new GeneralResponse(false, "Enroll not found", 404);
        return await _enrollRepository.UpdateEnroll(enrollModel);
    }

    public async Task<GeneralResponse> DeleteEnroll(int id)
    {
        var enroll = await _enrollRepository.GetEnrollById(id);
        if (enroll == null)
            return new GeneralResponse(false, "Enroll not found", 404);
        return await _enrollRepository.DeleteEnroll(id);
    }
}
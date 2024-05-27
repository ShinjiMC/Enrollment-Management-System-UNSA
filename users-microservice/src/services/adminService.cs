using users_microservice.models;
using users_microservice.repositories;

namespace users_microservice.services;

public interface IAdminService
{
    Task<AdminModel> CreateUser(AdminModel user);
    Task<List<AdminModel>> GetUsers();
}

public class AdminService : IAdminService
{
    private readonly IAdminRepository _userRepository;

    public AdminService(IAdminRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AdminModel> CreateUser(AdminModel user)
    {
        return await _userRepository.CreateUser(user);
    }
    public async Task<List<AdminModel>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }
}
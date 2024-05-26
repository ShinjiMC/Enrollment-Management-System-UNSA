using users_microservice.models;
using users_microservice.repositories;

namespace users_microservice.services;

public interface IUserService
{
    Task<UserModel> CreateUser(UserModel user);
    Task<List<UserModel>> GetUsers();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel> CreateUser(UserModel user)
    {
        return await _userRepository.CreateUser(user);
    }
    public async Task<List<UserModel>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }
}
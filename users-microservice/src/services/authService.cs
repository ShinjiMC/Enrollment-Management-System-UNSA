using users_microservice.authentication;
using users_microservice.models;
using users_microservice.repositories;

namespace users_microservice.services;

public interface ILoginService
{
    Task<string> SignInAsync(int userId, string username);
}

public class LoginService : ILoginService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IAdminRepository _adminRepository;

    public LoginService(IJwtProvider jwtProvider, IAdminRepository adminRepository)
    {
        _jwtProvider = jwtProvider;
        _adminRepository = adminRepository;
    }

    public async Task<string> SignInAsync(int userId, string username)
    {
        var customer = await _adminRepository.GetCustomerByIdAsync(userId);
        if (customer is null)
        {
            throw new InvalidOperationException($"Customer with id: '{userId}' was not found.");
        }

        if (customer.UserName != username)
        {
            throw new InvalidOperationException($"Customer with id: '{userId}' has not username: '{username}'.");
        }

        return _jwtProvider.Generate(userId, username);
    }
}
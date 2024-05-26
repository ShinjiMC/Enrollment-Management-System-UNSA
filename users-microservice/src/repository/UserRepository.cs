using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.repositories;

public interface IUserRepository
{
    Task<UserModel> CreateUser(UserModel user);
    Task<List<UserModel>> GetUsers();
}

public class UserRepository : IUserRepository
{
    private readonly MySqlContext _context;
    public UserRepository(MySqlContext context)
    {
        _context = context;
    }

    public async Task<UserModel> CreateUser(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<UserModel>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
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
    /*
    private int nextId = 3;
    private static readonly List<UserModel> Users = [
        new() {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            Password = "password1"
        },
        new() {
            Id = 2,
            Name = "Jane Doe",
            Email = "jane@example.com",
            Password = "password2"
        }
    ];

    public async Task<UserModel> CreateUser(UserModel user)
    {
        await Task.Delay(10); // Simulación de operación asincrónica
        user.Id = nextId;
        nextId += 1;
        Users.Add(user);
        return user;
    }

    public async Task<List<UserModel>> GetUsers()
    {
        await Task.Delay(10); // Simulación de operación asincrónica
        if (Users.Count == 0)
        {
            return [];
        }
        return Users;
    }
*/
}
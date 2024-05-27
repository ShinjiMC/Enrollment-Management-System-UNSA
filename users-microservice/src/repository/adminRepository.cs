using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.repositories;

public interface IAdminRepository
{
    Task<AdminModel> CreateUser(AdminModel user);
    Task<List<AdminModel>> GetUsers();
    Task<AdminModel> GetCustomerByIdAsync(int customerId);
    Task<AdminModel> GetAdminByUserNameAndPassword(string user, string password);
}

public class AdminRepository : IAdminRepository
{
    private readonly MySqlContext _context;
    public AdminRepository(MySqlContext context)
    {
        _context = context;
    }

    public async Task<AdminModel> CreateUser(AdminModel user)
    {
        await _context.Admins.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<AdminModel>> GetUsers()
    {
        return await _context.Admins.ToListAsync();
    }

    public async Task<AdminModel> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Admins.FirstOrDefaultAsync(x => x.ID == customerId);
    }

    public async Task<AdminModel> GetAdminByUserNameAndPassword(string user, string password)
    {
        var admin = await _context.Admins.FirstOrDefaultAsync(x => x.UserName == user && x.Password == password);
        if (admin == null)
        {
            throw new InvalidOperationException("Admin not found.");
        }
        return admin;
    }
}
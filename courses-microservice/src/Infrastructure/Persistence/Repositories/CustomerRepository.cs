using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Customer customer) => _context.Customers.Add(customer);
    public void Delete(Customer customer) => _context.Customers.Remove(customer);
    public void Update(Customer customer) => _context.Customers.Update(customer);
    public async Task<bool> ExistsAsync(Guid id) => await _context.Customers.AnyAsync(customer => customer.Id == id);
    public async Task<Customer?> GetByIdAsync(Guid id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Customer>> GetAll() => await _context.Customers.ToListAsync();
}
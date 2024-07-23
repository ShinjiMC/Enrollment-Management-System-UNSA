namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAll();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}
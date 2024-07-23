using NotificationsMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}

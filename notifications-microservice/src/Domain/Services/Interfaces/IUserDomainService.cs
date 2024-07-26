using NotificationsMicroservice.Domain.Entities;

namespace NotificationsMicroservice.Domain.Services.Interfaces
{
    public interface IUserDomainService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}

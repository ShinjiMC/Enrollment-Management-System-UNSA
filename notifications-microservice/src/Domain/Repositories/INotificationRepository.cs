using NotificationsMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(int id);
        Task<Notification> CreateAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id);
    }
}

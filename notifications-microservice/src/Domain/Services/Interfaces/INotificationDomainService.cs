using NotificationsMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Domain.Services.Interfaces
{
    public interface INotificationDomainService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
    }
}

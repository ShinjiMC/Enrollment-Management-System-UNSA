using NotificationsMicroservice.Domain.Entities;

namespace NotificationsMicroservice.Domain.Services.Interfaces
{
    public interface INotificationDomainService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<List<Notification>> GetByRecipientIdAsync(int recipientId);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
    }
}

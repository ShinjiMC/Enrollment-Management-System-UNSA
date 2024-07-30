using NotificationsMicroservice.Domain.Entities;

namespace NotificationsMicroservice.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(int id);
        Task<List<Notification>> GetByRecipientIdAsync(int recipientId);
        Task<int> GetNextIdAsync();
        Task<Notification> CreateAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id);
    }
}

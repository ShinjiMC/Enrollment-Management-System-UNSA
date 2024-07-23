
using NotificationsMicroservice.Domain.Entities;
using NotificationsMicroservice.Domain.Repositories;
using NotificationsMicroservice.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Domain.Services.Implementations
{
    public class NotificationDomainService : INotificationDomainService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationDomainService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            return await _notificationRepository.CreateAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task DeleteNotificationAsync(int id)
        {
            await _notificationRepository.DeleteAsync(id);
        }
    }
}

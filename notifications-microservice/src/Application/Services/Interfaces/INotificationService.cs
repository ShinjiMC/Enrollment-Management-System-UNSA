using NotificationsMicroservice.Application.Dtos;

namespace NotificationsMicroservice.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationsAsync();
        Task<NotificationDto> GetNotificationByIdAsync(int id);
        Task<List<NotificationDto>> GetByRecipientIdAsync(int recipientId);
        Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto);
        Task UpdateNotificationAsync(NotificationDto notificationDto);
        Task DeleteNotificationAsync(int id);
    }
}


using NotificationsMicroservice.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationsAsync();
        Task<NotificationDto> GetNotificationByIdAsync(int id);
        Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto);
        Task UpdateNotificationAsync(NotificationDto notificationDto);
        Task DeleteNotificationAsync(int id);
    }
}

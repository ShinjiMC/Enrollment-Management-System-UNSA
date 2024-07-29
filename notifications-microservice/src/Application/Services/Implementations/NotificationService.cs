using NotificationsMicroservice.Application.Dtos;
using NotificationsMicroservice.Application.Services.Interfaces;
using NotificationsMicroservice.Domain.Entities; // Asegura que esté incluido si usas Notification
using NotificationsMicroservice.Domain.Services.Interfaces;
using NotificationsMicroservice.Domain.ValueObjects; // Inclúyelo si Message es un Value Object

namespace NotificationsMicroservice.Application.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDomainService _notificationDomainService;

        public NotificationService(INotificationDomainService notificationDomainService)
        {
            _notificationDomainService = notificationDomainService;
        }

        public async Task<List<NotificationDto>> GetAllNotificationsAsync()
        {
            var notifications = await _notificationDomainService.GetAllNotificationsAsync();
            return notifications.Select(notification => new NotificationDto
            {
                Id = notification.Id,
                Type = notification.Type,
                Message = notification.Message.Content,
                Status = notification.Status,
                RecipientId = notification.RecipientId
            }).ToList();
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            var notification = await _notificationDomainService.GetNotificationByIdAsync(id);
            return new NotificationDto
            {
                Id = notification.Id,
                Type = notification.Type,
                Message = notification.Message.Content,
                Status = notification.Status,
                RecipientId = notification.RecipientId
            };
        }

        public async Task<List<NotificationDto>> GetByRecipientIdAsync(int recipientId)
        {
            var notifications = await _notificationDomainService.GetByRecipientIdAsync(recipientId);
            if (notifications == null || !notifications.Any())
                return null;
            return notifications.Select(notification => new NotificationDto
            {
                Id = notification.Id,
                Type = notification.Type,
                Message = notification.Message.Content,
                Status = notification.Status,
                RecipientId = notification.RecipientId
            }).ToList();
        }

        public async Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                Type = notificationDto.Type,
                Message = new Message(notificationDto.Message),
                Status = notificationDto.Status,
                RecipientId = notificationDto.RecipientId
            };
            var createdNotification = await _notificationDomainService.CreateNotificationAsync(notification);
            return new NotificationDto
            {
                Id = createdNotification.Id,
                Type = createdNotification.Type,
                Message = createdNotification.Message.Content,
                Status = createdNotification.Status,
                RecipientId = createdNotification.RecipientId
            };
        }

        public async Task UpdateNotificationAsync(NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                Id = notificationDto.Id,
                Type = notificationDto.Type,
                Message = new Message(notificationDto.Message),
                Status = notificationDto.Status,
                RecipientId = notificationDto.RecipientId
            };
            await _notificationDomainService.UpdateNotificationAsync(notification);
        }

        public async Task DeleteNotificationAsync(int id)
        {
            await _notificationDomainService.DeleteNotificationAsync(id);
        }
    }
}


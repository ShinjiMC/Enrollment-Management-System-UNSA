using Microsoft.AspNetCore.Mvc;
using NotificationsMicroservice.Application.Dtos;
using NotificationsMicroservice.Application.Services.Interfaces;

namespace NotificationsMicroservice.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notificationService, IUserService userService)
        {
            _notificationService = notificationService;
            _userService = userService; // Inicializa el servicio en el constructor
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
                return NotFound();

            return Ok(notification);
        }

        [HttpGet("recipient/{recipientId}")]
        public async Task<IActionResult> GetByRecipientId(int recipientId)
        {
            var notifications = await _notificationService.GetByRecipientIdAsync(recipientId);
            if (notifications == null || !notifications.Any())
                return NotFound("No notifications found.");
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
        {
            var userDto = await _userService.GetUserByIdAsync(notificationDto.RecipientId);
            if (userDto == null)
                return NotFound("User does not have configured notifications");
        
            if (!userDto.IsActive)
                return BadRequest("User does not have notifications activated");

            // Set the status to "Sent" if it is not provided
            notificationDto.Status = string.IsNullOrWhiteSpace(notificationDto.Status) ? "Sent" : notificationDto.Status;
            
            var createdNotification = await _notificationService.CreateNotificationAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotificationById), new { id = createdNotification.Id }, createdNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationDto notificationDto)
        {
            if (id != notificationDto.Id)
                return BadRequest("ID mismatch");

            await _notificationService.UpdateNotificationAsync(notificationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }
}

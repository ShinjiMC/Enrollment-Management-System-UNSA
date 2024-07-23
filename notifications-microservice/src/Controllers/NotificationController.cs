using Microsoft.AspNetCore.Mvc;
using NotificationsMicroservice.Application.Dtos;
using NotificationsMicroservice.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
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

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
        {
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

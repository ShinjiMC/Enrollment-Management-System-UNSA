using NotificationsMicroservice.Domain.ValueObjects;

namespace NotificationsMicroservice.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public Message Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;// E.g., "Sent", "Failed"
        public int RecipientId { get; set; } // User ID
    }
}

using NotificationsMicroservice.Domain.ValueObjects;

namespace NotificationsMicroservice.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Message Message { get; set; }
        public string Status { get; set; } // E.g., "Sent", "Failed"
        public int RecipientId { get; set; } // User ID
    }
}

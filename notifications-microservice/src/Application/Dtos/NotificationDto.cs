namespace NotificationsMicroservice.Application.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } // E.g., "Sent", "Failed"
        public int RecipientId { get; set; } // User ID
    }
}

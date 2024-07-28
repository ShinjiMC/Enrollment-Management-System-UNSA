namespace NotificationsMicroservice.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Preference { get; set; } = string.Empty; // "email" or "sms"
        public string ContactInfo { get; set; } = string.Empty;// Email or phone number
        public bool IsActive { get; set; } // Whether notifications are active
    }
}

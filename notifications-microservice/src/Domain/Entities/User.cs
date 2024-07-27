namespace NotificationsMicroservice.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Preference { get; set; } // "email" or "sms"
        public string ContactInfo { get; set; } // Email or phone number
        public bool IsActive { get; set; } // Whether notifications are active
    }
}
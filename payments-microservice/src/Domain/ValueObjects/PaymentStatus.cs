using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class PaymentStatus
    {
        [BsonElement("status")]
        public string Status { get; }

        public PaymentStatus(string status)
        {
            Status = status;
        }
    }
}

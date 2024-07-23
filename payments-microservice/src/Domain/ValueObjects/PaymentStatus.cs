using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class PaymentStatus
    {
        [BsonElement("status"), DefaultValue("Pending")]
        public string Status { get; }

        public PaymentStatus(string status)
        {
            Status = status;
        }
    }
}

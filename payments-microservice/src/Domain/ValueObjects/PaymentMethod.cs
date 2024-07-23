using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class PaymentMethod
    {
        [BsonElement("methodType"), BsonRepresentation(BsonType.String)]
        public string MethodType { get; }

        [BsonElement("details"), BsonRepresentation(BsonType.String)]
        public string Details { get; }

        public PaymentMethod(string methodType, string details)
        {
            MethodType = methodType;
            Details = details;
        }
    }
}
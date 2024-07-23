using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsMicroservice.Domain.Entities
{
    public class Payer
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? PayerId { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }
        
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        
        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string PhoneNumber { get; set; }
    }
}
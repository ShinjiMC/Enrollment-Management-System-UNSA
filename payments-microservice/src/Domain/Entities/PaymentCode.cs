using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class PaymentCode
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? PaymentCodeId { get; set; }

        [BsonElement("code"), BsonRepresentation(BsonType.String)]
        public string Code { get; set; }

        [BsonElement("studentId")]
        public string StudentId { get; set; }

        [BsonElement("electronicBillId")]
        public string ElectronicBillId { get; set; }

        [BsonElement("isUsed")]
        public bool IsUsed { get; set; }

    }
}


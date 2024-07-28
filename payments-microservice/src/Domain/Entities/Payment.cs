using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class Payment
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? PaymentId { get; set; }

        [BsonElement("amount")]
        public Money Amount { get; set; }

        [BsonElement("paymentDate")]
        public DateTime PaymentDate { get; set; }

        [BsonElement("paymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }

        [BsonElement("studentId")]
        public string StudentId { get; set; }

        [BsonElement("electronicBillId")]
        public string ElectronicBillId { get; set; }

        [BsonElement("status")]
        public PaymentStatus Status { get; set; }

    }

}

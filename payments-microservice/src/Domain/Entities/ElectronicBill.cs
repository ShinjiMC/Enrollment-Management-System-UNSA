using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class ElectronicBill
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ElectronicBillId { get; set; }

        [BsonElement("studentId")]
        public required string StudentId { get; set; }

        [BsonElement("totalAmount")]
        public required Money TotalAmount { get; set; }

        [BsonElement("dueDate")]
        public DateTime DueDate { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("status")]
        public required string Status { get; set; }

        [BsonElement("items")]
        public required List<ElectronicBillItem> Items { get; set; }
    }

    public class ElectronicBillItem
    {
        [BsonElement("electronicBillItemId")]
        public string? ElectronicBillItemId { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("amount")]
        public required Money Amount { get; set; }
    }
}

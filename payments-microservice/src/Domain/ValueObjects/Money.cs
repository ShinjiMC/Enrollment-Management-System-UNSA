using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class Money
    {
        [BsonElement("amount"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; }

        [BsonElement("currency"), BsonRepresentation(BsonType.String)]
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}

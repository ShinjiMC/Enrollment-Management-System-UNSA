using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class PaymentCode
    {
        public string Code { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public Amount Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime GeneratedAt { get; private set; }

        public PaymentCode(Amount amount, DateTime expirationDate)
        {
            Code = GenerateCode();
            Amount = amount;
            ExpirationDate = expirationDate;
            Status = PaymentStatus.Pending;
            GeneratedAt = DateTime.UtcNow;
        }

        private string GenerateCode()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

}

using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class PaymentReceipt
    {
        public Guid ReceiptId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Amount Amount { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public string IssuedTo { get; private set; }

        public PaymentReceipt(Guid paymentId, Amount amount, string issuedTo)
        {
            ReceiptId = Guid.NewGuid();
            PaymentId = paymentId;
            Amount = amount;
            ReceiptDate = DateTime.UtcNow;
            IssuedTo = issuedTo;
        }
    }

}

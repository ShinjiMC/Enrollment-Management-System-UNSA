using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class Payment
{
    public Guid PaymentId { get; private set; }
    public Guid StudentId { get; private set; }
    public Amount Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Payment(Guid studentId, Amount amount)
    {
        PaymentId = Guid.NewGuid();
        StudentId = studentId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(PaymentStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}

}

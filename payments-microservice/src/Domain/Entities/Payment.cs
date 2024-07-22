using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class Payment
    {
        public string PaymentId { get; private set; }
        public Money Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public string StudentId { get; private set; }
        public string ElectronicBillId { get; private set; }
        public PaymentStatus Status { get; private set; }

        public Payment(string paymentId, Money amount, DateTime paymentDate, PaymentMethod paymentMethod, string studentId, string electronicBillId, PaymentStatus status)
        {
            PaymentId = paymentId;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            StudentId = studentId;
            ElectronicBillId = electronicBillId;
            Status = status;
        }

        public void UpdateStatus(PaymentStatus newStatus)
        {
            Status = newStatus;
        }
    }

}

using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public Money Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string StudentId { get; set; }
        public string ElectronicBillId { get; set; }
        public PaymentStatus Status { get; set; }

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

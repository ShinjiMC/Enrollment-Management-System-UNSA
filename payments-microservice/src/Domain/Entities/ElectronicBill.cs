using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class ElectronicBill
    {
        public string ElectronicBillId { get; set; }
        public string StudentId { get; set; }
        public Money TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public ElectronicBillStatus Status { get; set; }
        public List<ElectronicBillItem> Items { get; set; }

        public void UpdateStatus(ElectronicBillStatus newStatus)
        {
            Status = newStatus;
        }
    }
}

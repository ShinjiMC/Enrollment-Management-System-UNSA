using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class ElectronicBill
    {
        public string ElectronicBillId { get; private set; }
        public string StudentId { get; private set; }
        public Money TotalAmount { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public ElectronicBillStatus Status { get; private set; }
        public List<ElectronicBillItem> Items { get; private set; }

        public ElectronicBill(string electronicBillId, string studentId, Money totalAmount, DateTime dueDate, DateTime createdDate, ElectronicBillStatus status, List<ElectronicBillItem> items)
        {
            ElectronicBillId = electronicBillId;
            StudentId = studentId;
            TotalAmount = totalAmount;
            DueDate = dueDate;
            CreatedDate = createdDate;
            Status = status;
            Items = items;
        }

        public void UpdateStatus(ElectronicBillStatus newStatus)
        {
            Status = newStatus;
        }
    }
}

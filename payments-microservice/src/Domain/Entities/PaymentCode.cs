using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class PaymentCode
    {
        public string PaymentCodeId { get; set; }
        public string Code { get; set; }
        public string StudentId { get; set; }
        public string ElectronicBillId { get; set; }
        public bool IsUsed { get; set; }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }
    }
}


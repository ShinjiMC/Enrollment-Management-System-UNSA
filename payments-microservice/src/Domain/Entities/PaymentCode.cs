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

        public PaymentCode(string paymentCodeId, string code, string studentId, string electronicBillId, bool isUsed)
        {
            PaymentCodeId = paymentCodeId;
            Code = code;
            StudentId = studentId;
            ElectronicBillId = electronicBillId;
            IsUsed = isUsed;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }
    }
}


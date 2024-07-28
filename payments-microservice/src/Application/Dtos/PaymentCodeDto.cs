namespace PaymentsMicroservice.Application.Dtos
{
    public class PaymentCodeDto
    {
        public string? PaymentCodeId { get; set; }
        public string? Code { get; set; }
        public string StudentId { get; set; }
        public string ElectronicBillId { get; set; }
        public bool IsUsed { get; set; }
    }
}

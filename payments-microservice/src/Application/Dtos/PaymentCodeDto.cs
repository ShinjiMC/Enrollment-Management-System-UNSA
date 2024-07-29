namespace PaymentsMicroservice.Application.Dtos
{
    public class PaymentCodeDto
    {
        public string? PaymentCodeId { get; set; }
        public string? Code { get; set; }
        public required string StudentId { get; set; }
        public required string ElectronicBillId { get; set; }
        public bool IsUsed { get; set; }
    }
}

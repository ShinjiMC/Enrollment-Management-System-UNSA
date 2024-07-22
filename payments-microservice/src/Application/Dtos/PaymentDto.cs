namespace PaymentsMicroservice.Application.Dtos
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string StudentId { get; set; }
        public string ElectronicBillId { get; set; }
        public string Status { get; set; }
    }
}

namespace PaymentsMicroservice.Application.Dtos
{
    public class PaymentDto
    {
        public string? PaymentId { get; set; }
        public MoneyDto Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethodDto PaymentMethod { get; set; }
        public string StudentId { get; set; }
        public string ElectronicBillId { get; set; }
        public PaymentStatusDto Status { get; set; }
    }

    public class MoneyDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class PaymentMethodDto
    {
        public string MethodType { get; set; }
        public string Details { get; set; }
    }

    public class PaymentStatusDto
    {
        public string Status { get; set; }
    }
}

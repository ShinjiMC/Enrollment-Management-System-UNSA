namespace PaymentsMicroservice.Controllers
{
    public class UpdateStatusRequest
    {
        public required string Status { get; set; }
    }

    public class PaymentCodeRequest
    {
        public required string StudentId { get; set; }
        public required string ElectronicBillId { get; set; }
    }
}
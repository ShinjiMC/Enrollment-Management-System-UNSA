namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class ElectronicBillStatus
    {
        public string Status { get; }

        public ElectronicBillStatus(string status)
        {
            Status = status;
        }
    }
}

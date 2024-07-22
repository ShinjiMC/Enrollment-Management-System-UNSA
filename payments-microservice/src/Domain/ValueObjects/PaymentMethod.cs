namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class PaymentMethod
    {
        public string MethodType { get; }
        public string Details { get; }

        public PaymentMethod(string methodType, string details)
        {
            MethodType = methodType;
            Details = details;
        }
    }
}
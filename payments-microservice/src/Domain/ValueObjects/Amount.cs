namespace PaymentsMicroservice.Domain.ValueObjects
{
    public class Amount
    {
        public decimal Value { get; private set; }
        public string Currency { get; private set; }

        public Amount(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }

}

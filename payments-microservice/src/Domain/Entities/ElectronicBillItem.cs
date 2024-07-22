using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class ElectronicBillItem
    {
        public string ElectronicBillItemId { get; set; }
        public string CourseId { get; set; }
        public string Description { get; set; }
        public Money Amount { get; set; }
    }
}

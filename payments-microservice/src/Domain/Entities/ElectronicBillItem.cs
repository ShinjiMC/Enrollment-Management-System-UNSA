using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Entities
{
    public class ElectronicBillItem
    {
        public string ElectronicBillItemId { get; private set; }
        public string CourseId { get; private set; }
        public string Description { get; private set; }
        public Money Amount { get; private set; }

        public ElectronicBillItem(string electronicBillItemId, string courseId, string description, Money amount)
        {
            ElectronicBillItemId = electronicBillItemId;
            CourseId = courseId;
            Description = description;
            Amount = amount;
        }
    }
}

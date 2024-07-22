namespace PaymentsMicroservice.Domain.Services.Implementations
{
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;
    using PaymentsMicroservice.Domain.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ElectronicBillDomainService : IElectronicBillDomainService
    {
        public ElectronicBill CreateElectronicBill(string studentId, List<ElectronicBillItem> electronicBillItems)
        {
            return new ElectronicBill
            {
                ElectronicBillId = Guid.NewGuid().ToString(),
                StudentId = studentId,
                Items = electronicBillItems,
                TotalAmount = new Money(electronicBillItems.Sum(item => item.Amount.Amount), "USD"),
                DueDate = DateTime.Now.AddMonths(1),
                CreatedDate = DateTime.Now,
                Status = new ElectronicBillStatus("Unpaid")
            };
        }

        public ElectronicBill UpdateElectronicBill(string electronicBillId, List<ElectronicBillItem> electronicBillItems)
        {
            return new ElectronicBill
            {
                ElectronicBillId = electronicBillId,
                Items = electronicBillItems,
                TotalAmount = new Money(electronicBillItems.Sum(item => item.Amount.Amount), "USD"),
                Status = new ElectronicBillStatus("Unpaid") // Assuming update resets status
            };
        }

        public ElectronicBillStatus CheckElectronicBillStatus(string electronicBillId)
        {
            // Logic for checking status of electronic bill
            return new ElectronicBillStatus("Unpaid");
        }
    }
}

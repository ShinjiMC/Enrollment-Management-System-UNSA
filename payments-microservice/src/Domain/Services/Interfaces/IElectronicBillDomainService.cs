namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;
    using System.Collections.Generic;

    public interface IElectronicBillDomainService
    {
        ElectronicBill CreateElectronicBill(string studentId, List<ElectronicBillItem> electronicBillItems);
        ElectronicBill UpdateElectronicBill(string electronicBillId, List<ElectronicBillItem> electronicBillItems);
        ElectronicBillStatus CheckElectronicBillStatus(string electronicBillId);
    }
}

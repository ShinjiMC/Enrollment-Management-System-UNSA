namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;
    using System;
    using System.Threading.Tasks;

    public interface IElectronicBillDomainService
    {
        Task<ElectronicBill> CreateElectronicBill(string studentId, Money totalAmount, DateTime dueDate, List<ElectronicBillItem> items);
        Task<ElectronicBill> GetElectronicBillById(string electronicBillId);
        Task<List<ElectronicBill>> GetElectronicBills();
        Task<bool> UpdateElectronicBillStatus(string electronicBillId, ElectronicBillStatus status);
    }
}

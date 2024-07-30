using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IElectronicBillRepository
    {
        Task<ElectronicBill> GetElectronicBillById(string electronicBillId);
        Task SaveElectronicBill(ElectronicBill electronicBill);
        Task<List<ElectronicBill>> GetElectronicBills();
    }
}

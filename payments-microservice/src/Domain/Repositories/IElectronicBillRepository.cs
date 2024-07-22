using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IElectronicBillRepository
    {
        ElectronicBill GetElectronicBillById(string electronicBillId);
        void SaveElectronicBill(ElectronicBill electronicBill);
    }
}

using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;

namespace PaymentsMicroservice.Repositories.Implementations
{
    public class ElectronicBillRepository : IElectronicBillRepository
    {
        private readonly List<ElectronicBill> _electronicBills = new List<ElectronicBill>();

        public ElectronicBill GetElectronicBillById(string electronicBillId)
        {
            return _electronicBills.SingleOrDefault(e => e.ElectronicBillId == electronicBillId);
        }

        public void SaveElectronicBill(ElectronicBill electronicBill)
        {
            var existingBill = _electronicBills.SingleOrDefault(e => e.ElectronicBillId == electronicBill.ElectronicBillId);
            if (existingBill != null)
            {
                _electronicBills.Remove(existingBill);
            }
            _electronicBills.Add(electronicBill);
        }
    }
}

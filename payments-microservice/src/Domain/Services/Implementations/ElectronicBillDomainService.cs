using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class ElectronicBillDomainService : IElectronicBillDomainService
    {
        private readonly IElectronicBillRepository _electronicBillRepository;

        public ElectronicBillDomainService(IElectronicBillRepository electronicBillRepository)
        {
            _electronicBillRepository = electronicBillRepository;
        }

        public async Task<ElectronicBill> CreateElectronicBill(string studentId, Money totalAmount, DateTime dueDate, List<ElectronicBillItem> items)
        {
            var electronicBill = new ElectronicBill
            {
                ElectronicBillId = Guid.NewGuid().ToString(), // Or use a different ID generation strategy
                StudentId = studentId,
                TotalAmount = totalAmount,
                DueDate = dueDate,
                CreatedDate = DateTime.UtcNow,
                Status = ElectronicBillStatus.Pending.ToString(), // Example default status
                Items = items
            };

            await _electronicBillRepository.SaveElectronicBill(electronicBill);
            return electronicBill;
        }

        public async Task<ElectronicBill> GetElectronicBillById(string electronicBillId)
        {
            return await _electronicBillRepository.GetElectronicBillById(electronicBillId);
        }

        public async Task<List<ElectronicBill>> GetElectronicBills()
        {
            var electronicBills = await _electronicBillRepository.GetElectronicBills();
            if (electronicBills == null)
            {
                return new List<ElectronicBill>();
            }
            return electronicBills;
        }

        public async Task<bool> UpdateElectronicBillStatus(string electronicBillId, ElectronicBillStatus status)
        {
            var electronicBill = await _electronicBillRepository.GetElectronicBillById(electronicBillId);
            if (electronicBill == null)
            {
                return false;
            }

            electronicBill.Status = status.ToString();
            await _electronicBillRepository.SaveElectronicBill(electronicBill);
            return true;
        }
    }
}

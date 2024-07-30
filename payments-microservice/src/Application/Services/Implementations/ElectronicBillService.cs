using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Mapping;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Domain.ValueObjects;


namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class ElectronicBillService : IElectronicBillService
    {
        private readonly IElectronicBillDomainService _electronicBillDomainService;

        public ElectronicBillService(IElectronicBillDomainService electronicBillDomainService)
        {
            _electronicBillDomainService = electronicBillDomainService;
        }

        public async Task<ElectronicBillDto> CreateElectronicBill(ElectronicBillDto electronicBillDto)
        {
            var electronicBill = await _electronicBillDomainService.CreateElectronicBill(
                electronicBillDto.StudentId,
                new Money(electronicBillDto.TotalAmount.Amount, electronicBillDto.TotalAmount.Currency),
                electronicBillDto.DueDate,
                electronicBillDto.Items?.ConvertAll(item => new ElectronicBillItem
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    Description = item.Description,
                    Amount = new Money(item.Amount.Amount, item.Amount.Currency)
                }) ?? new List<ElectronicBillItem>()
            );

            return ElectronicBillMapper.ToDto(electronicBill);
        }

        public async Task<ElectronicBillDto> GetElectronicBillById(string electronicBillId)
        {
            var electronicBill = await _electronicBillDomainService.GetElectronicBillById(electronicBillId);
            return ElectronicBillMapper.ToDto(electronicBill);
        }

        public async Task<List<ElectronicBillDto>> GetElectronicBills()
        {
            var electronicBills = await _electronicBillDomainService.GetElectronicBills();
            // itera
            return electronicBills.ConvertAll(ElectronicBillMapper.ToDto);
        }

        public async Task<bool> UpdateElectronicBillStatus(string electronicBillId, string status)
        {
            if (Enum.TryParse<ElectronicBillStatus>(status, out var electronicBillStatus))
            {
                return await _electronicBillDomainService.UpdateElectronicBillStatus(electronicBillId, electronicBillStatus);
            }
            return false;
        }
    }
}

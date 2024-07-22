using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Implementations;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class ElectronicBillService : IElectronicBillService
    {
        private readonly IElectronicBillRepository _electronicBillRepository;
        private readonly ElectronicBillManagementService _electronicBillManagementService;

        public ElectronicBillService(IElectronicBillRepository electronicBillRepository, ElectronicBillManagementService electronicBillManagementService)
        {
            _electronicBillRepository = electronicBillRepository;
            _electronicBillManagementService = electronicBillManagementService;
        }

        public ElectronicBillDto CreateElectronicBill(string studentId, List<ElectronicBillItemDto> items)
        {
            var billItems = items.ConvertAll(item => new ElectronicBillItem(
                item.ElectronicBillItemId,
                item.CourseId,
                item.Description,
                new Money(item.Amount, "USD") // Asumir USD para simplicidad
            ));

            var electronicBill = _electronicBillManagementService.CreateElectronicBill(studentId, billItems);
            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = electronicBill.TotalAmount.Amount,
                Currency = electronicBill.TotalAmount.Currency,
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.Status,
                Items = electronicBill.Items.ConvertAll(item => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    CourseId = item.CourseId,
                    Description = item.Description,
                    Amount = item.Amount.Amount
                })
            };
        }

        public ElectronicBillDto GetElectronicBillById(string electronicBillId)
        {
            var electronicBill = _electronicBillRepository.GetElectronicBillById(electronicBillId);
            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = electronicBill.TotalAmount.Amount,
                Currency = electronicBill.TotalAmount.Currency,
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.Status,
                Items = electronicBill.Items.ConvertAll(item => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    CourseId = item.CourseId,
                    Description = item.Description,
                    Amount = item.Amount.Amount
                })
            };
        }

        public ElectronicBillDto UpdateElectronicBill(string electronicBillId, List<ElectronicBillItemDto> items)
        {
            var billItems = items.ConvertAll(item => new ElectronicBillItem(
                item.ElectronicBillItemId,
                item.CourseId,
                item.Description,
                new Money(item.Amount, "USD")
            ));

            var electronicBill = _electronicBillManagementService.UpdateElectronicBill(electronicBillId, billItems);
            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = electronicBill.TotalAmount.Amount,
                Currency = electronicBill.TotalAmount.Currency,
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.Status,
                Items = electronicBill.Items.ConvertAll(item => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    CourseId = item.CourseId,
                    Description = item.Description,
                    Amount = item.Amount.Amount
                })
            };
        }
    }
}

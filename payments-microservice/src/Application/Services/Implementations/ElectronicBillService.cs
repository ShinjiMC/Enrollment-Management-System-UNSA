namespace PaymentsMicroservice.Application.Services.Implementations
{
    using PaymentsMicroservice.Application.Services.Interfaces;
    using PaymentsMicroservice.Domain.Services.Interfaces;
    using PaymentsMicroservice.Application.Dtos;
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;
    using System.Collections.Generic;
    using System.Linq;

    public class ElectronicBillService : IElectronicBillService
    {
        private readonly IElectronicBillDomainService _electronicBillDomainService;

        public ElectronicBillService(IElectronicBillDomainService electronicBillDomainService)
        {
            _electronicBillDomainService = electronicBillDomainService;
        }

        public ElectronicBillDto CreateElectronicBill(string studentId, List<ElectronicBillItemDto> electronicBillItems)
        {
            var domainItems = electronicBillItems.Select(item => new ElectronicBillItem
            {
                ElectronicBillItemId = item.ElectronicBillItemId,
                CourseId = item.CourseId,
                Description = item.Description,
                Amount = new Money(item.Amount, "USD")
            }).ToList();

            var electronicBill = _electronicBillDomainService.CreateElectronicBill(studentId, domainItems);

            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = electronicBill.TotalAmount.Amount,
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.Status,
                Items = electronicBill.Items.Select(i => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = i.ElectronicBillItemId,
                    CourseId = i.CourseId,
                    Description = i.Description,
                    Amount = i.Amount.Amount
                }).ToList()
            };
        }

        public ElectronicBillDto UpdateElectronicBill(string electronicBillId, List<ElectronicBillItemDto> electronicBillItems)
        {
            var domainItems = electronicBillItems.Select(item => new ElectronicBillItem
            {
                ElectronicBillItemId = item.ElectronicBillItemId,
                CourseId = item.CourseId,
                Description = item.Description,
                Amount = new Money(item.Amount, "USD")
            }).ToList();

            var electronicBill = _electronicBillDomainService.UpdateElectronicBill(electronicBillId, domainItems);

            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = electronicBill.TotalAmount.Amount,
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.Status,
                Items = electronicBill.Items.Select(i => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = i.ElectronicBillItemId,
                    CourseId = i.CourseId,
                    Description = i.Description,
                    Amount = i.Amount.Amount
                }).ToList()
            };
        }

        public string CheckElectronicBillStatus(string electronicBillId)
        {
            var status = _electronicBillDomainService.CheckElectronicBillStatus(electronicBillId);
            return status.Status;
        }
    }
}

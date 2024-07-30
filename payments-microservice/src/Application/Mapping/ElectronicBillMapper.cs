namespace PaymentsMicroservice.Application.Mapping
{
    using PaymentsMicroservice.Application.Dtos;
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ElectronicBillMapper
    {
        public static ElectronicBillDto ToDto(ElectronicBill electronicBill)
        {
            ArgumentNullException.ThrowIfNull(electronicBill);

            return new ElectronicBillDto
            {
                ElectronicBillId = electronicBill.ElectronicBillId,
                StudentId = electronicBill.StudentId,
                TotalAmount = new MoneyDto
                {
                    Amount = electronicBill.TotalAmount.Amount,
                    Currency = electronicBill.TotalAmount.Currency
                },
                DueDate = electronicBill.DueDate,
                CreatedDate = electronicBill.CreatedDate,
                Status = electronicBill.Status.ToString(),
                Items = electronicBill.Items?.Select(item => new ElectronicBillItemDto
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    Description = item.Description,
                    Amount = new MoneyDto
                    {
                        Amount = item.Amount.Amount,
                        Currency = item.Amount.Currency
                    }
                }).ToList() ?? new List<ElectronicBillItemDto>() // Ensures Items is never null
            };
        }

        public static ElectronicBill ToEntity(ElectronicBillDto electronicBillDto)
        {
            if (electronicBillDto == null)
            {
                throw new ArgumentNullException(nameof(electronicBillDto));
            }

            if (!Enum.TryParse<ElectronicBillStatus>(electronicBillDto.Status, out var status))
            {
                throw new ArgumentException($"Invalid status: {electronicBillDto.Status}");
            }

            return new ElectronicBill
            {
                ElectronicBillId = electronicBillDto.ElectronicBillId,
                StudentId = electronicBillDto.StudentId,
                TotalAmount = new Money(electronicBillDto.TotalAmount.Amount, electronicBillDto.TotalAmount.Currency),
                DueDate = electronicBillDto.DueDate,
                CreatedDate = electronicBillDto.CreatedDate,
                Status = electronicBillDto.Status, // Correctly set the enum value
                Items = electronicBillDto.Items?.Select(item => new ElectronicBillItem
                {
                    ElectronicBillItemId = item.ElectronicBillItemId,
                    Description = item.Description,
                    Amount = new Money(item.Amount.Amount, item.Amount.Currency)
                }).ToList() ?? [] // Ensures Items is never null
            };
        }
    }
}

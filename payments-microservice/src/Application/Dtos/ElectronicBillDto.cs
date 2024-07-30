namespace PaymentsMicroservice.Application.Dtos
{
    using System;
    using System.Collections.Generic;

    public class ElectronicBillDto
    {
        public string? ElectronicBillId { get; set; }
        public required string StudentId { get; set; }
        public required MoneyDto TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string Status { get; set; }
        public required List<ElectronicBillItemDto> Items { get; set; }
    }

    public class ElectronicBillItemDto
    {
        public required string ElectronicBillItemId { get; set; }
        public required string Description { get; set; }
        public required MoneyDto Amount { get; set; }
    }
}

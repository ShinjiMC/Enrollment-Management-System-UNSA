using System;
using System.Collections.Generic;

namespace PaymentsMicroservice.Application.Dtos
{
    public class ElectronicBillDto
    {
        public string ElectronicBillId { get; set; }
        public string StudentId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public List<ElectronicBillItemDto> Items { get; set; }
    }

    public class ElectronicBillItemDto
    {
        public string ElectronicBillItemId { get; set; }
        public string CourseId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}

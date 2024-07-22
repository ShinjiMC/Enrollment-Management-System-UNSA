namespace PaymentsMicroservice.Application.Services.Interfaces
{
    using PaymentsMicroservice.Application.Dtos;
    using System.Collections.Generic;

    public interface IElectronicBillService
    {
        ElectronicBillDto CreateElectronicBill(string studentId, List<ElectronicBillItemDto> electronicBillItems);
        ElectronicBillDto UpdateElectronicBill(string electronicBillId, List<ElectronicBillItemDto> electronicBillItems);
        string CheckElectronicBillStatus(string electronicBillId);
    }
}

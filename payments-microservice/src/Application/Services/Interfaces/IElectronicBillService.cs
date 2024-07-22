using PaymentsMicroservice.Application.Dtos;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IElectronicBillService
    {
        ElectronicBillDto CreateElectronicBill(string studentId, List<ElectronicBillItemDto> items);
        ElectronicBillDto GetElectronicBillById(string electronicBillId);
        ElectronicBillDto UpdateElectronicBill(string electronicBillId, List<ElectronicBillItemDto> items);
    }
}

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    using PaymentsMicroservice.Application.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IElectronicBillService
    {
        Task<ElectronicBillDto> CreateElectronicBill(ElectronicBillDto electronicBillDto);
        Task<ElectronicBillDto> GetElectronicBillById(string electronicBillId);
        Task<List<ElectronicBillDto>> GetElectronicBills();
        Task<bool> UpdateElectronicBillStatus(string electronicBillId, string status);
    }
}

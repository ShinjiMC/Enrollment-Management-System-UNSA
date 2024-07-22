using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class ElectronicBillManagementService
    {
        public ElectronicBill CreateElectronicBill(string studentId, List<ElectronicBillItem> electronicBillItems)
        {
            // Lógica para crear una boleta electrónica
            var totalAmount = CalculateTotalAmount(electronicBillItems);
            return new ElectronicBill(
                "newId", // Generar un ID único
                studentId,
                totalAmount,
                DateTime.Now.AddDays(30), // Fecha de vencimiento
                DateTime.Now,
                new ElectronicBillStatus("unpaid"),
                electronicBillItems
            );
        }

        public ElectronicBill UpdateElectronicBill(string electronicBillId, List<ElectronicBillItem> electronicBillItems)
        {
            // Lógica para actualizar una boleta electrónica
            var totalAmount = CalculateTotalAmount(electronicBillItems);
            return new ElectronicBill(
                electronicBillId,
                "studentId",
                totalAmount,
                DateTime.Now.AddDays(30),
                DateTime.Now,
                new ElectronicBillStatus("unpaid"),
                electronicBillItems
            );
        }

        public ElectronicBillStatus CheckElectronicBillStatus(string electronicBillId)
        {
            // Lógica para verificar el estado de una boleta electrónica
            return new ElectronicBillStatus("paid");
        }

        private Money CalculateTotalAmount(List<ElectronicBillItem> items)
        {
            decimal totalAmount = 0;
            foreach (var item in items)
            {
                totalAmount += item.Amount.Amount;
            }
            return new Money(totalAmount, "USD"); // Asumir USD para simplicidad
        }
    }
}

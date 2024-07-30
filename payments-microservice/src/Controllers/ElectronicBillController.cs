using Microsoft.AspNetCore.Mvc;
using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;

namespace PaymentsMicroservice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ElectronicBillController : ControllerBase
    {
        private readonly IElectronicBillService _electronicBillService;

        public ElectronicBillController(IElectronicBillService electronicBillService)
        {
            _electronicBillService = electronicBillService;
        }

        [HttpGet("{electronicBillId}")] // Route: api/electronicbill/{electronicBillId}
        public async Task<ActionResult<ElectronicBillDto>> GetElectronicBillById(string electronicBillId)
        {
            try {
                var electronicBill = await _electronicBillService.GetElectronicBillById(electronicBillId);
                return Ok(electronicBill);
            } catch (System.Exception e) {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost] // Route: api/electronicbill
        public async Task<ActionResult<ElectronicBillDto>> CreateElectronicBill(ElectronicBillDto electronicBillDto)
        {
            try {
                var createdElectronicBill = await _electronicBillService.CreateElectronicBill(electronicBillDto);
                return CreatedAtAction(nameof(GetElectronicBillById), new { electronicBillId = createdElectronicBill.ElectronicBillId }, createdElectronicBill);
            } catch (System.Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet] // Route: api/electronicbill
        public async Task<ActionResult<List<ElectronicBillDto>>> GetElectronicBills()
        {
            var electronicBills = await _electronicBillService.GetElectronicBills();
            if (electronicBills == null)
            {
                return NotFound(new { message = "No se encontraron facturas electrónicas" });
            }
            return Ok(electronicBills);
        }

        [HttpPut("{electronicBillId}/status")] // Route: api/electronicbill/{electronicBillId}/status
        public async Task<IActionResult> UpdateElectronicBillStatus(string electronicBillId, [FromBody] UpdateStatusRequest request)
        {
            var updated = await _electronicBillService.UpdateElectronicBillStatus(electronicBillId, request.Status);
            if (!updated)
            {
                return NotFound("Cannot update electronic bill status");
            }
            return Ok("Electronic bill status updated");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;

namespace PaymentsMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost] // Route: POST api/payment
        public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto paymentDto)
        {
            try {
                var createdPayment = await _paymentService.CreatePayment(paymentDto);
                return CreatedAtAction(nameof(GetPaymentById), new { paymentId = createdPayment.PaymentId }, createdPayment);
            } catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{paymentId}")] // Route: GET api/payment/{paymentId}
        public async Task<ActionResult<PaymentDto>> GetPaymentById(string paymentId)
        {
            try {
                var payment = await _paymentService.GetPaymentById(paymentId);
                return Ok(payment);
            } catch (Exception ex) {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{paymentId}/status")] // Route: PUT api/payment/{paymentId}/status
        public async Task<IActionResult> UpdatePaymentStatus(string paymentId, [FromBody] string status)
        {
            var updated = await _paymentService.UpdatePaymentStatus(paymentId, status);
            if (!updated)
            {
                return NotFound(new { message = "No se puede actualizar el estado del pago" });
            }
            return Ok(new { message = "Estado del pago actualizado" });
        }
    }
}

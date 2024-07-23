using Microsoft.AspNetCore.Mvc;
using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;

namespace PaymentsMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var createdPayment = await _paymentService.CreatePayment(paymentDto);
            return Ok(createdPayment);
             
        }

        [HttpGet("{paymentId}")] // Route: GET api/payment/{paymentId}
        public async Task<ActionResult<PaymentDto>> GetPaymentById(string paymentId)
        {
            var payment = await _paymentService.GetPaymentById(paymentId);
            if (payment == null)
            {
                return NotFound("Payment not found");
            }
            return Ok(payment);
        }

        [HttpPut("{paymentId}/status")] // Route: PUT api/payment/{paymentId}/status
        public async Task<IActionResult> UpdatePaymentStatus(string paymentId, [FromBody] string status)
        {
            var updated = await _paymentService.UpdatePaymentStatus(paymentId, status);
            if (!updated)
            {
                return NotFound("Cannot update payment status");
            }
            return Ok("Payment status updated");
        }
    }
}

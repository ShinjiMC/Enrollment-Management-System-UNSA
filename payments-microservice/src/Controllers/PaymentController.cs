namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Services.Interfaces;
    using PaymentsMicroservice.Application.Dtos;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")] // Route: api/payment
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto createPaymentDto)
        {
            if (createPaymentDto == null)
                return BadRequest("Invalid payment data");

            var payment = await _paymentService.CreatePaymentAsync(createPaymentDto);
            return Ok(payment);
        }

        [HttpPost("{paymentId}/confirm")]
        public async Task<IActionResult> ConfirmPayment(Guid paymentId)
        {
            var payment = await _paymentService.ConfirmPaymentAsync(paymentId);
            if (payment == null)
                return NotFound("Payment not found");

            return Ok(payment);
        }

        [HttpGet("{paymentId}")] // Route: api/payment/{paymentId}
        public async Task<IActionResult> GetPaymentById(Guid paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
                return NotFound("Payment not found");

            return Ok(payment);
        }
    }
}

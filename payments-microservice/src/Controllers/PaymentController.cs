using Microsoft.AspNetCore.Mvc;
using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;

namespace PaymentsMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(string id)
        {
            var paymentDto = _paymentService.GetPaymentById(id);
            if (paymentDto == null)
            {
                return NotFound();
            }

            return Ok(paymentDto);
        }

        [HttpPost]
        public IActionResult ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null)
            {
                return BadRequest("Invalid payment data.");
            }

            _paymentService.ProcessPayment(paymentDto);
            return Ok("Payment processed successfully.");
        }
    }
}

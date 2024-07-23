namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Dtos;
    using PaymentsMicroservice.Application.Mapping;
    using PaymentsMicroservice.Domain.Repositories;

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("{paymentId}")] // Route: api/payment/{paymentId}
        public ActionResult<PaymentDto> GetPaymentById(string paymentId)
        {
            var payment = _paymentRepository.GetPaymentById(paymentId).Result;
            if (payment == null)
            {
                return NotFound();
            }
            var paymentDto = PaymentMapper.ToDto(payment);
            return Ok(paymentDto);
        }

        [HttpPost] // Route: api/payment
        public ActionResult<string> SavePayment(PaymentDto paymentDto)
        {
            // comprobamos que el pago no sea nulo
            if (paymentDto == null)
            {
                return BadRequest("Payment is null");
            }
            var payment = PaymentMapper.ToEntity(paymentDto);
            _paymentRepository.SavePayment(payment);
            return Ok("Payment saved successfully");
        }
    }
}

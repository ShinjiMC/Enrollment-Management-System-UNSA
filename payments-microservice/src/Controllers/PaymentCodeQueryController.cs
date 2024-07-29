namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Dtos;
    using PaymentsMicroservice.Application.Services.Interfaces;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentCodeQueryController : ControllerBase
    {
        private readonly IPaymentCodeService _paymentCodeService;

        public PaymentCodeQueryController(IPaymentCodeService paymentCodeService)
        {
            _paymentCodeService = paymentCodeService;
        }

        [HttpGet("{paymentCodeId}")] // Route: api/v1/paymentcodequery/{paymentCodeId}
        public ActionResult<PaymentCodeDto> GetPaymentCodeById(string paymentCodeId)
        {
            var paymentCode = _paymentCodeService.GetPaymentCodeById(paymentCodeId);
            if (paymentCode == null)
            {
                return NotFound("Payment code not found.");
            }
            return Ok(paymentCode);
        }

        [HttpGet] // Route: api/v1/paymentcodequery
        public ActionResult<List<PaymentCodeDto>> GetPaymentCodes()
        {
            var paymentCodes = _paymentCodeService.GetPaymentCodes();
            if (paymentCodes == null)
            {
                return NotFound("No payment codes found.");
            }
            return Ok(paymentCodes);
        }
    }
}

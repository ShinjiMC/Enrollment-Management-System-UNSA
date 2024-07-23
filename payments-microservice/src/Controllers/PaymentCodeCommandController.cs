namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Dtos;
    using PaymentsMicroservice.Application.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCodeCommandController : ControllerBase
    {
        private readonly IPaymentCodeService _paymentCodeService;

        public PaymentCodeCommandController(IPaymentCodeService paymentService)
        {
            _paymentCodeService = paymentService;
        }

        [HttpPost] // Route: api/paymentcodecommand
        public ActionResult<string> GeneratePaymentCode(PaymentCodeDto paymentCodeDto)
        {
            var codeGenerated = _paymentCodeService.GeneratePaymentCode(paymentCodeDto.StudentId, paymentCodeDto.ElectronicBillId);
            paymentCodeDto.Code = codeGenerated.Code;
            paymentCodeDto.IsUsed = codeGenerated.IsUsed;
            var saved = _paymentCodeService.SavePaymentCode(paymentCodeDto);
            if (!saved)
            {
                return StatusCode(500, "An error occurred while saving the payment code.");
            }
            return Ok(codeGenerated.Code);    
        }
    }
}

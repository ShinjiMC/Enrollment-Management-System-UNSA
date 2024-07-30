namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Services.Interfaces;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentCodeCommandController : ControllerBase
    {
        private readonly IPaymentCodeService _paymentCodeService;

        public PaymentCodeCommandController(IPaymentCodeService paymentService)
        {
            _paymentCodeService = paymentService;
        }

        [HttpPost] // Route: api/v1/paymentcodecommand
        public ActionResult<string> GeneratePaymentCode(PaymentCodeRequest paymentCodeRequest)
        {
            var codeGenerated = _paymentCodeService.GeneratePaymentCode(paymentCodeRequest.StudentId, paymentCodeRequest.ElectronicBillId);
            if (codeGenerated == null)
            {
                return StatusCode(400, new { Error = "Error al generar el código de pago. studentId o electronicBillId es inválido" });
            }
            var codeSaved = _paymentCodeService.SavePaymentCode(codeGenerated);
            if (!codeSaved)
            {
                return StatusCode(500, new { Error = "Error al almacenar el código de pago" });
            }
            return Ok(new { Code = codeGenerated.Code });
        }
    }
}

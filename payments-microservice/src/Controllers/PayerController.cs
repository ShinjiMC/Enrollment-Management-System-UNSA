namespace PaymentsMicroservice.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Services.Interfaces;
    using PaymentsMicroservice.Domain.Entities;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class PayerController : ControllerBase
    {
        private readonly IPayerService _payerService;

        public PayerController(IPayerService payerService)
        {
            _payerService = payerService;
        }

        [HttpGet] // Route: api/v1/payer
        public ActionResult<List<Payer>> GetPayers()
        {
            var payers = _payerService.GetPayers().Result;
            return Ok(payers);
        }
        
        [HttpGet("{payerId}")] // Route: api/v1/payer/{payerId}
        public async Task<ActionResult<Payer>> GetPayerById(string payerId)
        {
            var payer = await _payerService.GetPayerById(payerId);
            if (payer == null)
            {
                return NotFound(new { message = "Pagante no encontrado" }); 
            }
            return Ok(payer);
        }

        [HttpPost] // Route: api/v1/payer
        public ActionResult<string> SavePayer(Payer payer)
        {
            var result = _payerService.SavePayer(payer).Result;
            if (!result)
            {
                return BadRequest("Error al guardar pagante");
            }
            return Ok("Pagante guardado correctamente");
        }
    }
}
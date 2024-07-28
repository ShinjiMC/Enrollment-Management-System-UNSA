namespace PaymentsMicroservice.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.Repositories;

    [Route("api/[controller]")]
    [ApiController]
    public class PayerController : ControllerBase
    {
        private readonly IPayerRepository _payerRepository;

        public PayerController(IPayerRepository payerRepository)
        {
            _payerRepository = payerRepository;
        }

        [HttpGet] // Route: api/payer
        public ActionResult<List<Payer>> GetPayers()
        {
            var payers = _payerRepository.GetPayers().Result;
            return Ok(payers);
        }
        
        [HttpGet("{payerId}")] // Route: api/payer/{payerId}
        public ActionResult<Payer> GetPayerById(string payerId)
        {
            var payer =  _payerRepository.GetPayerById(payerId);
            if (payer == null)
            {
                return NotFound();
            }
            return Ok(payer);
        }

        [HttpPost] // Route: api/payer
        public ActionResult SavePayer(Payer payer)
        {
            _payerRepository.SavePayer(payer);
            return Ok("Payer saved successfully");
        }
    }
}
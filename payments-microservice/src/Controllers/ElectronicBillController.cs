namespace PaymentsMicroservice.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentsMicroservice.Application.Services.Interfaces;
    using PaymentsMicroservice.Application.Dtos;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class ElectronicBillController : ControllerBase
    {
        private readonly IElectronicBillService _electronicBillService;

        public ElectronicBillController(IElectronicBillService electronicBillService)
        {
            _electronicBillService = electronicBillService;
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<ElectronicBillDto> CreateElectronicBill(string studentId, [FromBody] List<ElectronicBillItemDto> electronicBillItems)
        {
            var result = _electronicBillService.CreateElectronicBill(studentId, electronicBillItems);
            return Ok(result);
        }

        [HttpPut]
        [Route("update/{electronicBillId}")]
        public ActionResult<ElectronicBillDto> UpdateElectronicBill(string electronicBillId, [FromBody] List<ElectronicBillItemDto> electronicBillItems)
        {
            var result = _electronicBillService.UpdateElectronicBill(electronicBillId, electronicBillItems);
            return Ok(result);
        }

        [HttpGet]
        [Route("status/{electronicBillId}")] // Route: api/electronicbill/status/{electronicBillId}
        public ActionResult<string> CheckElectronicBillStatus(string electronicBillId)
        {
            var result = _electronicBillService.CheckElectronicBillStatus(electronicBillId);
            return Ok(result);
        }
    }
}

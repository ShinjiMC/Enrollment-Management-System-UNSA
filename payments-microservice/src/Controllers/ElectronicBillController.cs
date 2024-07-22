using Microsoft.AspNetCore.Mvc;
using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;

namespace PaymentsMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectronicBillsController : ControllerBase
    {
        private readonly IElectronicBillService _electronicBillService;

        public ElectronicBillsController(IElectronicBillService electronicBillService)
        {
            _electronicBillService = electronicBillService;
        }

        [HttpPost]
        public IActionResult CreateElectronicBill([FromBody] CreateElectronicBillRequest request)
        {
            if (request == null || request.Items == null)
            {
                return BadRequest("Invalid electronic bill data.");
            }

            var electronicBillDto = _electronicBillService.CreateElectronicBill(request.StudentId, request.Items);
            return Ok(electronicBillDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetElectronicBillById(string id)
        {
            var electronicBillDto = _electronicBillService.GetElectronicBillById(id);
            if (electronicBillDto == null)
            {
                return NotFound();
            }

            return Ok(electronicBillDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateElectronicBill(string id, [FromBody] UpdateElectronicBillRequest request)
        {
            if (request == null || request.Items == null)
            {
                return BadRequest("Invalid electronic bill data.");
            }

            var electronicBillDto = _electronicBillService.UpdateElectronicBill(id, request.Items);
            if (electronicBillDto == null)
            {
                return NotFound();
            }

            return Ok(electronicBillDto);
        }
    }

    public class CreateElectronicBillRequest
    {
        public string StudentId { get; set; }
        public List<ElectronicBillItemDto> Items { get; set; }
    }

    public class UpdateElectronicBillRequest
    {
        public List<ElectronicBillItemDto> Items { get; set; }
    }
}

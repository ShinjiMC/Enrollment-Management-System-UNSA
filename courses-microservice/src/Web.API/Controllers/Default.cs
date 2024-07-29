using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.API.Controllers
{
    [Route("")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var hola = "Hola";
            return Ok(hola);
        }
    }
}

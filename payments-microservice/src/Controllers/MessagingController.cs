using Microsoft.AspNetCore.Mvc;
using Messaging; // Asegúrate de que el espacio de nombres sea correcto

namespace PaymentsMicroservice.Controllers
{
    public class SendMessageRequest
    {
        public string Message { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly Publisher _publisher;
        private readonly Consumer _consumer;

        public MessagingController(Publisher publisher, Consumer consumer)
        {
            _publisher = publisher;
            _consumer = consumer;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] SendMessageRequest request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("The message field is required.");
            }

            _publisher.SendMessage(request.Message);
            return Ok("Message sent.");
        }
        
        [HttpGet("receive")]
        public IActionResult ReceiveMessages()
        {
            // This method would typically be called asynchronously or in a different way
            // to continuously receive messages
            _consumer.ReceiveMessages();
            return Ok("Receiving messages. Check console output.");
        }
    }
}

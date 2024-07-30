using RabbitMQ.Client;
using System.Text;

namespace Messaging
{
    public class Publisher
    {
        private readonly IConfiguration _configuration;
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public Publisher(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostname = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672");
            _username = _configuration["RabbitMQ:UserName"] ?? "guest";
            _password = _configuration["RabbitMQ:Password"] ?? "guest";
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                Port = _port,
                UserName = _username,
                Password = _password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "my_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "my_queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

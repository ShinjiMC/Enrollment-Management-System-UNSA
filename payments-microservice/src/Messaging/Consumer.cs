using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Messaging
{
    public class Consumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public Consumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostname = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672");
            _username = _configuration["RabbitMQ:UserName"] ?? "guest";
            _password = _configuration["RabbitMQ:Password"] ?? "guest";
        }

        public void ReceiveMessages()
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "my_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}

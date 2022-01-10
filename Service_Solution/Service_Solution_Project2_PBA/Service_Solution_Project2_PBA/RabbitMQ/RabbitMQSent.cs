using System;
using RabbitMQ.Client;
using System.Text;

namespace Service_Solution_Project2_PBA
{
    class RabbitMQSent
    {
        public RabbitMQSent(string message)
        {
            var factory = new ConnectionFactory() { 
                UserName = "guest",
                Password = "guest",
                HostName = "localHost:5672"
        };
            using (var conn = factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

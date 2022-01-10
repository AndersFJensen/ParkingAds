using System;
using RabbitMQ.Client;
using System.Text;

namespace Service_Solution_Project2_PBA
{
    public class RabbitMQSent
    {
        public RabbitMQSent(string message)
        {
            var factory = new ConnectionFactory() { 
                UserName = "guest",
                Password = "guest",
                HostName = "localHost"
        };
            using (var conn = factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "TestQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello this is a message from 127.0.0.1",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

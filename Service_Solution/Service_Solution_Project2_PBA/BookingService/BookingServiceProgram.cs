using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BookingService
{
    class BookingServiceProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the BookingService!");
            RabbitMQRecieve rabbitMQ = new RabbitMQRecieve();
            while (true) ;
        }
    }

    class RabbitMQRecieve
    {
        public RabbitMQRecieve()
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };
            using (var conn = factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "SendFromClient",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);        //Sent message to mail server. 
                };
                channel.BasicConsume(queue: "SendFromClient",
                                 autoAck: true,
                                 consumer: consumer);
                Console.ReadLine();
            }


        }
    }
    static class RabbitMQSent
    {
        public static void SentMessageRabbitMQ(string message)
        {
            {
                var factory = new ConnectionFactory()
                {
                    UserName = "guest",
                    Password = "guest",
                    HostName = "localHost"
                };
                using (var conn = factory.CreateConnection())
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(queue: "TestQueueBookingService",
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
}

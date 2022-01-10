using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace Service_Solution_Project2_PBA
{
    public class RabbitMQRecieve
    {

        //Will keep running with the EventingBasicConsumer event.  
        public RabbitMQRecieve()
        {
            var factory = new ConnectionFactory()
            {
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
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);  
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
            }
        } 
        

    }
}

using System;
using RabbitMQ.Client;
using System.Text;

namespace Service_Solution_Project2_PBA
{
    public class RabbitMQSent
    {

        public void RabbitMQSendParkingAndAd(string parking, string ad, string userId)
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
                channel.QueueDeclare(queue: userId,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(parking + " " + ad);

                channel.BasicPublish(exchange: "",
                                     routingKey: userId,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0} and {1}", parking, ad);
                //Console.ReadLine();
            }


            

        }

    }

    
}

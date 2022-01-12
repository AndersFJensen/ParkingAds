using EmailService;
using NUnit.Framework;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MailServiceTestProject
{
    public class MailServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Sent_An_Email()
        {
            //Arrange
            string bookingMessage = "<div style='padding:20px;'><p>You booked a parkingspot! \n Regards The Grasshoppers.</p></div>"; 
            string htmlMessage = "<div style='padding:20px;font-family:verdana;text-align:center;background-color:#4bd30a;color:b42cf5'>The Beer so Good it’s Bad.</div>";
            string fullMessage = bookingMessage + htmlMessage; 
            //Act
            MailHandler.SentMail(fullMessage);   
            //Assert
            Assert.Pass();  //Should assert something else.
        }

        //Send til Rabboit MQ queue for this mail.
        [Test]
        public void Test_RabbitMQ_For_Sending_Mail()
        {
            //Arrange
            string bookingMessage = "<div style='padding:20px;'><p>You booked a parkingspot! \n Regards The Grasshoppers.</p></div>";
            string htmlMessage = "<div style='padding:20px;font-family:verdana;text-align:center;background-color:#4bd30a;color:b42cf5'>The Beer so Good it’s Bad.</div>";
            string fullMessage = bookingMessage + htmlMessage;
            //Act
            EmailService.RabbitMQRecieve rabbitMQRecieve = new RabbitMQRecieve();
            var factory = new ConnectionFactory()
            {
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

                var body = Encoding.UTF8.GetBytes(fullMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello this is a message from 127.0.0.1",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", fullMessage);
                Console.ReadLine();
            }
            //Assert
            Assert.Pass();
        }
    }
}
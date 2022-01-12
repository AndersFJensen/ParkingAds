using Client_Service_Project_2_PBA.RabbitMQ;
using System;

namespace Client_Service_Project_2_PBA
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQSent sendWithRabbitMQ = new RabbitMQSent();
            string message = "Message From Client";
            sendWithRabbitMQ.RabbitMQSend(message);
            RabbitMQReceive rabbitMQReceive = new RabbitMQReceive(message);
        }
    }
}

using Client_Service_Project_2_PBA.RabbitMQ;
using System;

namespace Client_Service_Project_2_PBA
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQSent sendWithRabbitMQ = new RabbitMQSent();
            while (true)
            {
                
                string message = Console.ReadLine();  
                sendWithRabbitMQ.RabbitMQSend(message);
            }

            //if(Console.ReadLine() == "Receive")
            //{
                //RabbitMQReceive rabbitMQReceive = new RabbitMQReceive(message);
            //}else if(Console.ReadLine() == "Send")
            //{

            //}

            
            
        }
    }
}

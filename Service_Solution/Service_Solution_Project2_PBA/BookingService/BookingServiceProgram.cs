using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Threading.Tasks;

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
                    Console.WriteLine(" [x] Received {0}", message);

                    CreateBookingModel(message);
                };
                channel.BasicConsume(queue: "SendFromClient",
                                 autoAck: true,
                                 consumer: consumer);
                Console.ReadLine();
            }
        }
        public async void CreateBookingModel(string message)
        {
            await MongoDBHandler.CreateBooking(null);       //Create bookingModel for saving.
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

    public class BookingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Price")]
        public string Price { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; }
        [BsonElement("License Plate Number")]
        public string LicensePlateNr { get; set; }
        [BsonElement("User")]
        public string User { get; set; }
    }

    public static class MongoDBHandler
    {
        public static async Task CreateBooking(BookingModel message)
        {
            BsonDocument bsonElement = new BsonDocument {
                {"","" },
                {"","" },
                {"","" },
                {"","" },
                {"","" }
            };
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017;DatebaseName:'BookingData';CollectionName:'Bookings'");
            var database = dbClient.GetDatabase("BookingsData");
            var collection = database.GetCollection<BsonDocument>("Bookings");
            try { await collection.InsertOneAsync(bsonElement);
                RabbitMQSent.SentMessageRabbitMQ(message.ToString());       
            }
            catch (Exception e) { throw new Exception("Couldnt insert the booking model to MongoDb! Try again or check for an error! "); }
        }
    }
}

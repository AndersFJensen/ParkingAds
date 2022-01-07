using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading;
using RabbitMQ.Client; 

namespace Service_Solution_Project2_PBA
{

    #region RedisCache
    class Program
    {
        [Serializable]
        private struct TestEntry
        {
            [JsonInclude]
            public string id;
            [JsonInclude]
            public string desc; 
            public TestEntry(string id, string desc)
            {
                this.id = id;
                this.desc = desc; 
            }
        }

        static async Task Main(string[] args)
        {
            //TODO: Benchmark with alot of entries without cache. 
            //Test with cache. 
            //Test with multiple server clients. 
            //Q1: "What should we do if the distributed cache are overqueried?"
            //Split code into seperate files. (Very importanté!).
            //Communicate with ParkingAd and ParkingService. (Very importanté!).

            RedisCache redisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:5002", 
                //InstanceName = "RedisCache_" + Guid.NewGuid().ToString(),
            });
            var TestEntry1 = new TestEntry("123", "It is working!");
            var TestEntry2 = new TestEntry("1234", "It is workingx!");
            var TestEntry3 = new TestEntry("12345", "It is workingy!");
            var TestEntry4 = new TestEntry("123456", "It is workingz!");
            await DistributedCacheExtensions.SetRecordAsync(redisCache, TestEntry1.id, TestEntry1);
            await DistributedCacheExtensions.SetRecordAsync(redisCache, TestEntry2.id, TestEntry2);
            await DistributedCacheExtensions.SetRecordAsync(redisCache, TestEntry3.id, TestEntry3);
            await DistributedCacheExtensions.SetRecordAsync(redisCache, TestEntry4.id, TestEntry4);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            List<TestEntry> list = new List<TestEntry>();
            list.Add(await DistributedCacheExtensions.GetRecordAsync<TestEntry>(redisCache, TestEntry1.id));
            list.Add(await DistributedCacheExtensions.GetRecordAsync<TestEntry>(redisCache, TestEntry2.id));
            list.Add(await DistributedCacheExtensions.GetRecordAsync<TestEntry>(redisCache, TestEntry3.id));
            list.Add(await DistributedCacheExtensions.GetRecordAsync<TestEntry>(redisCache, TestEntry4.id));
            if (list.Count != 0)
                foreach (TestEntry result in list)
                {
                    Console.WriteLine($"Entry with {result.id} and discription '{result.desc}' was found! \n");
                }
            else
            { Console.WriteLine("Not working!"); }
        }
    }

    public static class DistributedCacheExtensions
    {
        /**save entry to Redis method*/
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordID,                       
            T data,                                
            TimeSpan? absoluteExpireTime = null,    
            TimeSpan? unusedExpireTime = null)  
        {
            var options = new DistributedCacheEntryOptions();           
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60); 
            options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(3600);                 
     
            var jsonData = JsonSerializer.Serialize(data); 
            await cache.SetStringAsync(recordID, jsonData, options);
            Console.WriteLine($"Saved entry {jsonData} to Redis with id {recordID} at {DateTime.Now} \n");
        }

        /**get entry from Redis method*/
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            string jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                return default(T);  
            }

            Console.WriteLine($"Loaded entry {jsonData} from Redis with id {recordId} at {DateTime.Now} \n");
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }

    #endregion

    #region RabbitMQ

    class MessageHandler
    {
        ConnectionFactory factory = new ConnectionFactory();
        IConnection conn = null;
        IModel channelSent = null;
        IModel channelRecieve = null;
        //TODO: 
        //We need a consumer. Running async. 
        //Test the message system. 
        public MessageHandler()
        {
            Init();  
        } 
        public void Init()
        {
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.HostName = "localHost:5672";
            try
            {
                conn = factory.CreateConnection();
                channelSent = conn.CreateModel();
                channelSent.QueueDeclareNoWait("TestSent", true, false, false, null);
                channelRecieve = conn.CreateModel();
                channelRecieve.QueueDeclareNoWait("TestRecieve", true, false, false, null);
            }
            catch (Exception e)
            {
                throw e; 
            }

        }
        public void Disconnect()    //TODO: Should be able to sent a specific channel/conn to close. 
        {
            channelRecieve.Close();
            conn.Close();
            Console.WriteLine($"Disconnected from {factory.UserName}!\n host: {factory.HostName}");
        }

        public async Task<T> SentMessage<T>(T data) //TODO: We should be able to check the type and sent it to the right queue.
        {
            if (data is null)
            {
                return default(T);
            }
            

            return data; //Change
        }

    }

    #endregion
}

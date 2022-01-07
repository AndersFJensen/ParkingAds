using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json.Serialization;

namespace Service_Solution_Project2_PBA
{
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
            RedisCache redisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:5002", 
                InstanceName = "RedisCache_" + Guid.NewGuid().ToString(),
            });
            var TestEntry1 = new TestEntry("123", "It is working!");
            await DistributedCacheExtensions.SetRecordAsync(redisCache, TestEntry1.id, TestEntry1);
            var result = await DistributedCacheExtensions.GetRecordAsync<TestEntry>(redisCache, TestEntry1.id);
            if(!result.Equals(null))
            Console.WriteLine($"Entry with {result.id} and discription '{result.desc}' was found! \n");
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

    static class MessageHandler
    {
        //Listen - async --> Check message.

        //Sent - async

        //Check Redis database - async 
    }
}

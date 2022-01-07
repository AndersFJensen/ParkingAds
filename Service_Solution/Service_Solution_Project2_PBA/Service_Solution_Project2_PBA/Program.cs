using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading;

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

    static class MessageHandler
    {
        //Listen - async --> Check message.

        //Sent - async

        //Check Redis database - async 
    }
}

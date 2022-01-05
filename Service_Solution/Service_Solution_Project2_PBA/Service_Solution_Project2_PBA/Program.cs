﻿using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Service_Solution_Project2_PBA
{
    class Program
    {
        static IDistributedCache IDistcache = null; 
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = ConnectionToRedis.connString;
                options.InstanceName = ConnectionToRedis.instance_Name; 
            });
        }
    }

    public static class ConnectionToRedis
    {
        public const string instance_Name = "RedisDemo_";
        public const string connString = "localhost:5002"; //Mapped the redis port to docker. 

    }

    public static class DistributedCacheExtensions
    {
        //save entry to Redis method
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordID,                        //the key/unique identifier
            T data,                                 //whatever we store in the cache
            TimeSpan? absoluteExpireTime = null,    //setting null as default
            TimeSpan? unusedExpireTime = null)  
        {
            var options = new DistributedCacheEntryOptions();           //setup the options for the entries we put in the cache.
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60); //?? = if null use 60 secs
            options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(3600);                 //Not accesing for 1 hour.
          
            var jsonData = JsonSerializer.Serialize(data);  //The entry to Json data format.
            await cache.SetStringAsync(recordID, jsonData, options);
            Console.WriteLine($"Saved entry to Redis with id {recordID} at {DateTime.Now} \n");
        }

        //get entry from Redis method
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                return default(T);  //return the default value of an entry we pass in if not found. An object = null, int = 0 etc...
            }

            Console.WriteLine($"Loaded entry from Redis with id {recordId} at {DateTime.Now} \n");
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

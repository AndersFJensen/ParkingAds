using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace Service_Solution_Project2_PBA
{
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
}

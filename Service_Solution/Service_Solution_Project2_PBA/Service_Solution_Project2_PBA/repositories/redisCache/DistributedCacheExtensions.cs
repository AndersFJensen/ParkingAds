using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Service_Solution_Project2_PBA.repositories;
using Service_Solution_Project2_PBA.domain;

namespace Service_Solution_Project2_PBA
{
    public static class DistributedCacheExtensions
    {
        /**save entry to Redis method*/
        //public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
        //    string recordID,                       
        //    T data,                                
        //    TimeSpan? absoluteExpireTime = null,    
        //    TimeSpan? unusedExpireTime = null)  
        //{
        //    var options = new DistributedCacheEntryOptions();           
        //    options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60); 
        //    options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(3600);                 

        //    var jsonData = JsonSerializer.Serialize(data); 
        //    await cache.SetStringAsync(recordID, jsonData, options);
        //    Console.WriteLine($"Saved entry {jsonData} to Redis with id {recordID} at {DateTime.Now} \n");
        //}

        ///**get entry from Redis method*/
        //public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        //{
        //    string jsonData = await cache.GetStringAsync(recordId);
        //    if(jsonData is null)
        //    {

        //    }

        //    //await GetRecordAsyncIsDefault<string>(cache, recordId);

        //    Console.WriteLine($"Loaded entry {jsonData} from Redis with id {recordId} at {DateTime.Now} \n");
        //    return JsonSerializer.Deserialize<T>(jsonData);
        //}

        //private static async Task<T> GetRecordAsyncIsDefault<T>(IDistributedCache cache, string cacheRecord)
        //{
        //    bool result = false;
        //    if (cacheRecord is null)
        //    {

        //        await SetRecordAsync<string>(cache, recordId, );

        //    }
        //    return result;
        //}
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string userId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(3600);
            options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(3600);

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(userId, jsonData, options);
            Console.WriteLine($"Saved entry {jsonData} to Redis with id {userId} at {DateTime.Now} \n");
        }

        /**get entry from Redis method*/
        public static async Task<string> GetRecordAsync<T>(this IDistributedCache cache, string recordId, string cacheIdentifier)
        {
            
            string jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                jsonData = await GetRecordAsyncHelper(cache, recordId, cacheIdentifier);
                return jsonData;
            }
            Console.WriteLine($"Loaded entry {jsonData} from Redis with id {recordId} at {DateTime.Now} \n");
            return JsonSerializer.Deserialize<T>(jsonData).ToString();
        }

        private static async Task<string> GetRecordAsyncHelper(IDistributedCache cache, string cacheRecord, string cacheIdentifier)
        {
            if (cacheIdentifier == "Ad")
            {
                AdServiceServiceReposIF adService = new AdServiceServiceRepos();
                var data = await adService.CallAdServiceGET();
                await SetRecordAsync(cache, cacheRecord, data.body);
                return data.body;

            }
            else
            {
                ParkingAdServiceReposIF parkingAdService = new ParkingAdServiceRepos();
                var data = await parkingAdService.GetParkingAdServiceDataGET();
                await SetRecordAsync(cache, cacheRecord, data.body);
                return data.body;
            }
            

        }
    }
}

using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Service_Solution_Project2_PBA.repositories;
using Service_Solution_Project2_PBA.domain;

namespace Service_Solution_Project2_PBA.repositories.redisCache
{
    public static class DistributedCacheExtensionsParkingService
    {
        //public static async Task SetRecordAsyncParkingService<T>(this IDistributedCache cache,
        //    string userId,
        //    T data,
        //    TimeSpan? absoluteExpireTime = null,
        //    TimeSpan? unusedExpireTime = null)
        //{
        //    var options = new DistributedCacheEntryOptions();
        //    options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(3600);
        //    options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(3600);

        //    var jsonData = JsonSerializer.Serialize(data);
        //    await cache.SetStringAsync(userId, jsonData, options);
        //    Console.WriteLine($"Saved entry {jsonData} to Redis with id {userId} at {DateTime.Now} \n");
        //}

        ///**get entry from Redis method*/
        //public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        //{
        //    string jsonData = await cache.GetStringAsync(recordId);
        //    if (jsonData is null)
        //    {
        //        jsonData = await GetRecordAsyncHelper(cache, recordId);
        //    }
        //    Console.WriteLine($"Loaded entry {jsonData} from Redis with id {recordId} at {DateTime.Now} \n");
        //    return JsonSerializer.Deserialize<T>(jsonData);
        //}

        //private static async Task<string> GetRecordAsyncHelper(IDistributedCache cache, string cacheRecord)
        //{
        //    ParkingAdServiceReposIF parkingAdService = new ParkingAdServiceRepos();
        //    ParkingAdServiceMessageModel data = await parkingAdService.GetParkingAdServiceDataGET();
        //    await SetRecordAsyncParkingService(cache, cacheRecord, data.body);
        //    return data.body;
        //}

    }
}

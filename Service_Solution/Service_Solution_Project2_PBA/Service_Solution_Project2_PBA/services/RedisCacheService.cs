using Microsoft.Extensions.Caching.StackExchangeRedis;
using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.services
{
    public class RedisCacheService : RedisCacheServiceIF
    {
        private readonly RedisCache parkingRedisCache = null;
        private readonly RedisCache adRedisCache = null;
        public RedisCacheService()
        {
            parkingRedisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:5002",
                InstanceName = "RedisCache_ParkingService" ,
            });
            adRedisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:5002",
                InstanceName = "RedisCache_AdService",
            });
        }

        public async Task<T> RetrieveFromCacheAdService<T>(string recordId)
        {
            return await DistributedCacheExtensions.GetRecordAsync<T>(adRedisCache, recordId, "Ad");
        }

        public async Task<T> RetrieveFromCacheParkingService<T>(string recordId)
        {
            return await DistributedCacheExtensions.GetRecordAsync<T>(parkingRedisCache, recordId, "Parking");
            
        }

        public async Task SaveToCacheParkingService<T>(string recordId, T data)
        {
            await DistributedCacheExtensions.SetRecordAsync(parkingRedisCache, recordId, data);
        }
    }
}

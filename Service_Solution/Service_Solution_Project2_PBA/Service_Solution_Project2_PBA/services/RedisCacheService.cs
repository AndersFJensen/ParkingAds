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
        private readonly RedisCache redisCache = null;
        public RedisCacheService()
        {
            redisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:5002",
                //InstanceName = "RedisCache_" + Guid.NewGuid().ToString(),
            });
        }
        public async Task<T> RetrieveFromCache<T>(string recordId)
        {
            return await DistributedCacheExtensions.GetRecordAsync<T>(redisCache, recordId);
            
        }

        public async Task SaveToCache<T>(string recordId, T data)
        {
            await DistributedCacheExtensions.SetRecordAsync(redisCache, recordId, data);
        }
    }
}

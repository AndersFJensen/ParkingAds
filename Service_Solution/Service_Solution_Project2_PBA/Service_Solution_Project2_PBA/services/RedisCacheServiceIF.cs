using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.services
{
    interface RedisCacheServiceIF
    {
        public Task SaveToCache<T>(string recordId, T data);
        public Task<T> RetrieveFromCache<T>(string recordId);
    }
}

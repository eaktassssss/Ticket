using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Caching.Redis
{
    public interface IRedisCacheService
    {
        Task SetAsync(string key, object data, DistributedCacheEntryOptions option);
        Task SetAsync(string key, object data);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}

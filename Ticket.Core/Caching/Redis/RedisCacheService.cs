using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Caching.Redis
{
    public class RedisCacheService : IRedisCacheService
    {


        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task SetAsync(string key, object data, DistributedCacheEntryOptions option)
        {
            if (data == null)
            {
                return;
            }
            else
            {
                var model = JsonConvert.SerializeObject(data);
                await _distributedCache.SetStringAsync(key, model, option);
            }
        }
        public async Task SetAsync(string key, object data)
        {
            if (data == null)
            {
                return;
            }
            else
            {
                var model = JsonConvert.SerializeObject(data);
                await _distributedCache.SetStringAsync(key, model);
            }
        }
        public async Task<T> GetAsync<T>(string key)
        {

            var data = await _distributedCache.GetStringAsync(key);
            if (data == null)
            {
                return default;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }


        }
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
        public async Task RefreshAsync(string key)
        {
            await _distributedCache.RefreshAsync(key);
        }
    }
}

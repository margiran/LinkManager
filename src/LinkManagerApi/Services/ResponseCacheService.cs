using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Net.Mime;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace LinkManagerApi.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _cache;
        public ResponseCacheService(IDistributedCache cache)
        {
            _cache=cache;
        }
        public async Task CacheResponseAsync(string cacheKey, object response)
        {
            if (response == null)
            {
                return;
            }
            var serializedResponse =  JsonConvert.SerializeObject(response);
            await _cache.SetStringAsync(cacheKey,serializedResponse);
            // await _cache.SetAsync(cacheKey,serializedResponse, new DistributedCacheEntryOptions 
            // {
            //      AbsoluteExpirationRelativeToNow =new TimeSpan(600)
            // });


        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse= await _cache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
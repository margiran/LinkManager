using System;
using System.Threading.Tasks;
namespace LinkManagerApi.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response );
        Task<string> GetCachedResponseAsync(string cacheKey);
         
    }
}
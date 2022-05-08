using System.Threading;
using System.Threading.Tasks;
using LinkManagerApi.cache;
using LinkManagerApi.Dtos;
using LinkManagerApi.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LinkManagerApi.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : ICacheable   
      where TResponse : LinkResponseDto, new()

    {

        private readonly IResponseCacheService _cacheService;
        private readonly RedisCacheSettings _cacheSettings;

        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        public CachingBehavior(IResponseCacheService cacheService,RedisCacheSettings cacheSettings , ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _logger=logger;
            _cacheService=cacheService;
            _cacheSettings=cacheSettings;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestType= request.GetType();
            _logger.LogInformation($"{requestType} is caching.");

            TResponse response;
            if (!_cacheSettings.Enabled)
            {
               response= await next();
               return response;
            }
            var cacheResponse = await _cacheService.GetCachedResponseAsync(request.CacheKey); 
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                _logger.LogInformation($"get response from cache {request.CacheKey}");
                response =(TResponse) JsonConvert.DeserializeObject<LinkResponseDto> (cacheResponse);
                return  response;
            }

            _logger.LogInformation($"request {request.CacheKey} is not in cache");
            response= await next();
            await _cacheService.CacheResponseAsync(request.CacheKey,response);
            return response;
        }
    }
}
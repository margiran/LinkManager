using LinkManagerApi.Behaviors;
using LinkManagerApi.cache;
using LinkManagerApi.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.SetupServices
{
    public class CacheInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
        {
            var redisCacheSettings =new RedisCacheSettings();
            Configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if(!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.AddStackExchangeRedisCache(options=> options.Configuration=redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService,ResponseCacheService>();

        }
    }
}
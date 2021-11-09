using LinkManagerApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.SetupServices
{
    public class DbInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddSingleton<ILinkService,LinkService>();
        }
    }
}